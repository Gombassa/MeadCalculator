import { useState, useEffect, useCallback } from 'react'
import { ingredientService, calculatorService } from '../services/api'
import HoneySelector from '../components/HoneySelector'
import IngredientForm from '../components/IngredientForm'
import CalculationResults from '../components/CalculationResults'
import NutrientCalculator from '../components/NutrientCalculator'

export default function Calculator() {
  const [activeTab, setActiveTab] = useState('abv')
  const [honey, setHoney] = useState({ ingredientId: '', amount: '' })
  const [ingredients, setIngredients] = useState([])
  const [allIngredients, setAllIngredients] = useState([])
  const [honeys, setHoneys] = useState([])
  const [desiredVolume, setDesiredVolume] = useState('')
  const [desiredABV, setDesiredABV] = useState('')
  const [results, setResults] = useState(null)
  const [error, setError] = useState('')

  const loadIngredients = async () => {
    try {
      const response = await ingredientService.getAll()
      setAllIngredients(response.data)
      // Separate honeys from other ingredients (type 0 = Honey)
      const honeyList = response.data.filter(ing => ing.type === 0)
      const otherIngredients = response.data.filter(ing => ing.type !== 0)
      setHoneys(honeyList)
      // Note: honeys are separated, other ingredients stay in allIngredients for the form
    } catch (err) {
      setError('Failed to load ingredients: ' + err.message)
    }
  }

  // Real-time calculation
  const performCalculation = useCallback(async () => {
    // Reset error on new calculation
    setError('')

    // Prepare honey
    if (!honey.ingredientId || !honey.amount) {
      setResults(null)
      return
    }

    try {
      const selectedHoney = honeys.find(h => h.id === parseInt(honey.ingredientId))

      // Get additional ingredients (excluding honey)
      const additionalIngredients = ingredients.filter(ing => ing.ingredientId && ing.amount)

      // Find water ingredient (id = 41)
      const water = allIngredients.find(ing => ing.id === 41)

      const calculationIngredients = [
        {
          ingredientId: parseInt(honey.ingredientId),
          ingredientName: selectedHoney?.name,
          type: selectedHoney?.type,
          amount: parseFloat(honey.amount),
          sugarContentPercentage: selectedHoney?.medianSugarContent ?? selectedHoney?.sugarContentPercentage,
          unit: selectedHoney?.unit,
        },
        // Add other ingredients
        ...additionalIngredients.map(ing => {
          const selectedIngredient = allIngredients.find(
            ai => ai.id === parseInt(ing.ingredientId)
          )
          return {
            ingredientId: parseInt(ing.ingredientId),
            ingredientName: selectedIngredient?.name,
            type: selectedIngredient?.type,
            amount: parseFloat(ing.amount),
            sugarContentPercentage: selectedIngredient?.sugarContentPercentage,
            unit: selectedIngredient?.unit,
          }
        }),
        // Add water if desired volume or ABV is set and no other ingredients
        ...(((desiredVolume && parseFloat(desiredVolume) > 0) || (desiredABV && parseFloat(desiredABV) > 0)) &&
            additionalIngredients.length === 0 &&
            water
          ? [{
              ingredientId: water.id,
              ingredientName: water.name,
              type: water.type,
              amount: 0, // Will be calculated by backend
              sugarContentPercentage: water.sugarContentPercentage,
              unit: water.unit,
            }]
          : [])
      ]

      // Determine calculation mode based on inputs
      let mode = 0 // HoneyWeight
      let targetValue = null

      if (desiredVolume && parseFloat(desiredVolume) > 0) {
        mode = 2 // TargetVolume
        targetValue = parseFloat(desiredVolume)
      } else if (desiredABV && parseFloat(desiredABV) > 0) {
        mode = 1 // TargetABV
        targetValue = parseFloat(desiredABV)
      }

      const request = {
        ingredients: calculationIngredients,
        mode: mode,
        targetValue: targetValue,
      }

      const response = await calculatorService.calculate(request)
      setResults(response.data)
    } catch (err) {
      setError('Calculation failed: ' + (err.response?.data?.message || err.message))
      setResults(null)
    }
  }, [honey, ingredients, honeys, allIngredients, desiredVolume, desiredABV])

  const addIngredient = () => {
    setIngredients([
      ...ingredients,
      { id: Date.now(), ingredientId: '', amount: '', type: 'Honey' }
    ])
  }

  const removeIngredient = (id) => {
    setIngredients(ingredients.filter(ing => ing.id !== id))
  }

  const updateIngredient = (id, field, value) => {
    setIngredients(
      ingredients.map(ing =>
        ing.id === id ? { ...ing, [field]: value } : ing
      )
    )
  }

  const updateHoney = (field, value) => {
    setHoney(prev => ({ ...prev, [field]: value }))
  }

  // Load ingredients on mount
  useEffect(() => {
    loadIngredients()
  }, [])

  // Trigger calculation in real-time when inputs change
  useEffect(() => {
    const timer = setTimeout(() => {
      performCalculation()
    }, 300) // Debounce for 300ms to avoid too many API calls

    return () => clearTimeout(timer)
  }, [performCalculation])

  return (
    <div className="min-h-screen py-12 px-4" style={{ backgroundColor: '#20752b' }}>
      <div className="max-w-6xl mx-auto">
        <h1 className="text-4xl font-bold text-white mb-2">Mead Calculator</h1>
        <p className="text-gray-200 mb-8">
          Calculate your mead recipe with precision
        </p>

        {/* Tab Navigation */}
        <div className="flex gap-4 mb-8">
          <button
            onClick={() => setActiveTab('abv')}
            className={`px-6 py-3 rounded-lg font-semibold transition ${
              activeTab === 'abv'
                ? 'bg-gradient-to-r from-amber-600 to-yellow-600 text-white'
                : 'bg-white text-amber-900 border border-amber-300 hover:border-amber-600'
            }`}
          >
            ABV Calculator
          </button>
          <button
            onClick={() => setActiveTab('nutrients')}
            className={`px-6 py-3 rounded-lg font-semibold transition ${
              activeTab === 'nutrients'
                ? 'bg-gradient-to-r from-amber-600 to-yellow-600 text-white'
                : 'bg-white text-amber-900 border border-amber-300 hover:border-amber-600'
            }`}
          >
            Nutrient Calculator (SNA)
          </button>
        </div>

        {/* ABV Calculator Tab */}
        {activeTab === 'abv' && (
          <>
            {error && (
              <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded mb-6">
                {error}
              </div>
            )}

            {/* Honey Selection - Full Width First */}
            <div className="mb-8">
              <HoneySelector
                honey={honey}
                allHoneys={honeys}
                onUpdate={updateHoney}
              />
            </div>

            {/* Desired Volume and ABV Inputs */}
            <div className="grid md:grid-cols-2 gap-8 mb-8">
              <div className="bg-white rounded-lg shadow-lg p-6">
                <label className="block text-sm font-semibold text-amber-900 mb-2">
                  Desired Volume (ml)
                </label>
                <input
                  type="number"
                  value={desiredVolume}
                  onChange={(e) => setDesiredVolume(e.target.value)}
                  placeholder="e.g., 5000"
                  step="100"
                  className="w-full border border-amber-300 rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-amber-500"
                />
                <p className="text-xs text-gray-500 mt-2">Leave empty to calculate from honey weight</p>
              </div>

              <div className="bg-white rounded-lg shadow-lg p-6">
                <label className="block text-sm font-semibold text-amber-900 mb-2">
                  Desired ABV (%)
                </label>
                <input
                  type="number"
                  value={desiredABV}
                  onChange={(e) => setDesiredABV(e.target.value)}
                  placeholder="e.g., 12"
                  step="0.5"
                  className="w-full border border-amber-300 rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-amber-500"
                />
                <p className="text-xs text-gray-500 mt-2">Leave empty to calculate from honey weight</p>
              </div>
            </div>

            <div className="grid lg:grid-cols-3 gap-8">
              {/* Input Section */}
              <div className="lg:col-span-2">
                <div className="bg-white rounded-lg shadow-lg p-6">
                  <h2 className="text-2xl font-bold text-amber-900 mb-6">
                    Additional Ingredients
                  </h2>

                  {ingredients.length === 0 ? (
                    <p className="text-gray-500 text-center py-4 mb-6">
                      No additional ingredients added yet
                    </p>
                  ) : (
                    <div className="space-y-4 mb-6">
                      {ingredients.map(ing => (
                        <IngredientForm
                          key={ing.id}
                          ingredient={ing}
                          allIngredients={allIngredients}
                          onUpdate={updateIngredient}
                          onRemove={removeIngredient}
                        />
                      ))}
                    </div>
                  )}

                  <button
                    onClick={addIngredient}
                    className="w-full bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600 transition"
                  >
                    + Add Ingredient
                  </button>
                </div>
              </div>

              {/* Results Section */}
              <div>
                {results ? (
                  <CalculationResults results={results} />
                ) : (
                  <div className="bg-white rounded-lg shadow-lg p-6 text-center text-amber-700">
                    <p>Results will appear here</p>
                  </div>
                )}
              </div>
            </div>
          </>
        )}

        {/* Nutrient Calculator Tab */}
        {activeTab === 'nutrients' && (
          <NutrientCalculator specificGravity={results?.specificGravity} brix={results?.brix} />
        )}
      </div>
    </div>
  )
}
