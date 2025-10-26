import { useState, useEffect } from 'react'
import { ingredientService, calculatorService } from '../services/api'
import IngredientForm from '../components/IngredientForm'
import CalculationResults from '../components/CalculationResults'

export default function Calculator() {
  const [ingredients, setIngredients] = useState([])
  const [allIngredients, setAllIngredients] = useState([])
  const [calculationMode, setCalculationMode] = useState('HoneyWeight')
  const [targetValue, setTargetValue] = useState('')
  const [results, setResults] = useState(null)
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState('')

  useEffect(() => {
    loadIngredients()
  }, [])

  const loadIngredients = async () => {
    try {
      const response = await ingredientService.getAll()
      setAllIngredients(response.data)
    } catch (err) {
      setError('Failed to load ingredients: ' + err.message)
    }
  }

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

  const handleCalculate = async () => {
    setLoading(true)
    setError('')
    setResults(null)

    try {
      const calculationIngredients = ingredients
        .filter(ing => ing.ingredientId && ing.amount)
        .map(ing => {
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
        })

      if (calculationIngredients.length === 0) {
        setError('Please add at least one ingredient')
        return
      }

      const request = {
        ingredients: calculationIngredients,
        mode: calculationMode,
        targetValue: targetValue ? parseFloat(targetValue) : null,
      }

      const response = await calculatorService.calculate(request)
      setResults(response.data)
    } catch (err) {
      setError('Calculation failed: ' + (err.response?.data?.message || err.message))
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-amber-50 via-yellow-50 to-orange-50 py-12 px-4">
      <div className="max-w-6xl mx-auto">
        <h1 className="text-4xl font-bold text-amber-900 mb-2">Mead Calculator</h1>
        <p className="text-amber-700 mb-8">
          Calculate your mead recipe with precision
        </p>

        {error && (
          <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded mb-6">
            {error}
          </div>
        )}

        <div className="grid lg:grid-cols-3 gap-8">
          {/* Input Section */}
          <div className="lg:col-span-2">
            <div className="bg-white rounded-lg shadow-lg p-6 mb-6">
              <h2 className="text-2xl font-bold text-amber-900 mb-6">
                Recipe Ingredients
              </h2>

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

              <button
                onClick={addIngredient}
                className="w-full bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600 transition"
              >
                + Add Ingredient
              </button>
            </div>

            {/* Calculation Mode Section */}
            <div className="bg-white rounded-lg shadow-lg p-6">
              <h2 className="text-2xl font-bold text-amber-900 mb-6">
                Calculation Mode
              </h2>

              <div className="space-y-4">
                <div>
                  <label className="block text-sm font-semibold text-amber-900 mb-2">
                    Mode
                  </label>
                  <select
                    value={calculationMode}
                    onChange={(e) => setCalculationMode(e.target.value)}
                    className="w-full border border-amber-300 rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-amber-500"
                  >
                    <option value="HoneyWeight">
                      Calculate ABV from ingredients
                    </option>
                    <option value="TargetABV">
                      Target ABV (calculate honey needed)
                    </option>
                    <option value="TargetVolume">
                      Target Volume
                    </option>
                  </select>
                </div>

                {calculationMode !== 'HoneyWeight' && (
                  <div>
                    <label className="block text-sm font-semibold text-amber-900 mb-2">
                      {calculationMode === 'TargetABV' ? 'Target ABV (%)' : 'Target Volume (ml)'}
                    </label>
                    <input
                      type="number"
                      value={targetValue}
                      onChange={(e) => setTargetValue(e.target.value)}
                      placeholder={calculationMode === 'TargetABV' ? 'e.g., 12' : 'e.g., 5000'}
                      className="w-full border border-amber-300 rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-amber-500"
                    />
                  </div>
                )}

                <button
                  onClick={handleCalculate}
                  disabled={loading}
                  className="w-full bg-gradient-to-r from-amber-600 to-yellow-600 text-white px-4 py-3 rounded font-semibold hover:from-amber-700 hover:to-yellow-700 transition disabled:opacity-50"
                >
                  {loading ? 'Calculating...' : 'Calculate'}
                </button>
              </div>
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
      </div>
    </div>
  )
}
