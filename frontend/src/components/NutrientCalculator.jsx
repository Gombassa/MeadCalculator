import { useState } from 'react'
import { nutrientService } from '../services/api'

export default function NutrientCalculator({ specificGravity, brix }) {
  const [batchSizeLiters, setBatchSizeLiters] = useState('')
  const [yeastRequirement, setYeastRequirement] = useState('Low')
  const [additives, setAdditives] = useState({
    useGoFerm: true,
    useFermaidO: true,
    useFermaidK: true,
    useDAP: true,
  })
  const [numberOfSteps, setNumberOfSteps] = useState(3)
  const [schedule, setSchedule] = useState(null)
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState('')

  const yeastRequirementMap = {
    'Extra Low': 0,
    'Low': 1,
    'Medium': 2,
    'High': 3,
  }

  const handleGenerateSNA = async () => {
    setLoading(true)
    setError('')
    setSchedule(null)

    try {
      if (!batchSizeLiters || parseFloat(batchSizeLiters) <= 0) {
        setError('Please enter a valid batch size in liters')
        setLoading(false)
        return
      }

      if (!specificGravity || !brix) {
        setError('Please enter batch gravity and Brix first')
        setLoading(false)
        return
      }

      const additivesList = []
      if (additives.useGoFerm) additivesList.push(0)
      if (additives.useFermaidO) additivesList.push(2)
      if (additives.useFermaidK) additivesList.push(1)
      if (additives.useDAP) additivesList.push(3)

      if (additivesList.length === 0) {
        setError('Please select at least one additive to use')
        setLoading(false)
        return
      }

      const request = {
        specificGravity: parseFloat(specificGravity),
        brix: parseFloat(brix),
        yeastRequirement: yeastRequirementMap[yeastRequirement],
        batchSizeLiters: parseFloat(batchSizeLiters),
        additivesToUse: additivesList,
        useGoFerm: additives.useGoFerm,
        useFermaidO: additives.useFermaidO,
        useFermaidK: additives.useFermaidK,
        useDAP: additives.useDAP,
        numberOfSteps: parseInt(numberOfSteps),
      }

      const response = await nutrientService.generateSNA(request)
      setSchedule(response.data)
    } catch (err) {
      setError('SNA generation failed: ' + (err.response?.data || err.message))
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="space-y-6">
      {/* Input Section */}
      <div className="bg-white rounded-lg shadow-lg p-6">
        <h3 className="text-xl font-bold text-amber-900 mb-4">SNA (Staggered Nutrient Additions) Calculator</h3>

        {error && (
          <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded mb-4">
            {error}
          </div>
        )}

        <div className="grid md:grid-cols-2 gap-4 mb-6">
          {/* Batch Size */}
          <div>
            <label className="block text-sm font-semibold text-amber-900 mb-2">
              Batch Size (liters)
            </label>
            <input
              type="number"
              value={batchSizeLiters}
              onChange={(e) => setBatchSizeLiters(e.target.value)}
              placeholder="e.g., 20"
              className="w-full border border-amber-300 rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-amber-500"
            />
          </div>

          {/* Yeast Requirement */}
          <div>
            <label className="block text-sm font-semibold text-amber-900 mb-2">
              Yeast Nitrogen Requirement
            </label>
            <select
              value={yeastRequirement}
              onChange={(e) => setYeastRequirement(e.target.value)}
              className="w-full border border-amber-300 rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-amber-500"
            >
              <option value="Extra Low">Extra Low (0.5x)</option>
              <option value="Low">Low (0.75x)</option>
              <option value="Medium">Medium (0.90x)</option>
              <option value="High">High (1.25x)</option>
            </select>
          </div>
        </div>

        {/* Additives Selection */}
        <div className="bg-amber-50 rounded p-4 mb-6">
          <h4 className="font-semibold text-amber-900 mb-3">Nutrient Additives</h4>
          <div className="space-y-2">
            <label className="flex items-center">
              <input
                type="checkbox"
                checked={additives.useGoFerm}
                onChange={(e) => setAdditives({ ...additives, useGoFerm: e.target.checked })}
                className="w-4 h-4 rounded"
              />
              <span className="ml-2 text-amber-900">Go Ferm PE (Rehydration)</span>
            </label>
            <label className="flex items-center">
              <input
                type="checkbox"
                checked={additives.useFermaidO}
                onChange={(e) => setAdditives({ ...additives, useFermaidO: e.target.checked })}
                className="w-4 h-4 rounded"
              />
              <span className="ml-2 text-amber-900">Fermaid O (Organic)</span>
            </label>
            <label className="flex items-center">
              <input
                type="checkbox"
                checked={additives.useFermaidK}
                onChange={(e) => setAdditives({ ...additives, useFermaidK: e.target.checked })}
                className="w-4 h-4 rounded"
              />
              <span className="ml-2 text-amber-900">Fermaid K (Mixed)</span>
            </label>
            <label className="flex items-center">
              <input
                type="checkbox"
                checked={additives.useDAP}
                onChange={(e) => setAdditives({ ...additives, useDAP: e.target.checked })}
                className="w-4 h-4 rounded"
              />
              <span className="ml-2 text-amber-900">DAP (Inorganic)</span>
            </label>
          </div>
        </div>

        {/* Number of Steps */}
        <div className="mb-6">
          <label className="block text-sm font-semibold text-amber-900 mb-2">
            Number of Nutrient Additions (3-4 recommended)
          </label>
          <input
            type="number"
            min="2"
            max="6"
            value={numberOfSteps}
            onChange={(e) => setNumberOfSteps(e.target.value)}
            className="w-full border border-amber-300 rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-amber-500"
          />
        </div>

        {/* Generate Button */}
        <button
          onClick={handleGenerateSNA}
          disabled={loading}
          className="w-full bg-gradient-to-r from-amber-600 to-yellow-600 text-white px-4 py-3 rounded font-semibold hover:from-amber-700 hover:to-yellow-700 transition disabled:opacity-50"
        >
          {loading ? 'Generating SNA Schedule...' : 'Generate SNA Schedule'}
        </button>
      </div>

      {/* Schedule Display */}
      {schedule && (
        <div className="space-y-4">
          {/* YAN Summary */}
          <div className="bg-white rounded-lg shadow-lg p-6">
            <h3 className="text-lg font-bold text-amber-900 mb-4">YAN Requirements</h3>
            <div className="grid md:grid-cols-2 gap-4">
              <div className="bg-amber-50 p-4 rounded">
                <p className="text-sm text-amber-700">Total Sugar (g/L)</p>
                <p className="text-2xl font-bold text-amber-900">{schedule.yanCalculation.totalSugarPerLiter.toFixed(1)}</p>
              </div>
              <div className="bg-amber-50 p-4 rounded">
                <p className="text-sm text-amber-700">Required YAN (PPM)</p>
                <p className="text-2xl font-bold text-amber-900">{schedule.yanCalculation.requiredYAN.toFixed(1)}</p>
              </div>
              <div className="bg-amber-50 p-4 rounded">
                <p className="text-sm text-amber-700">Total YAN Added</p>
                <p className="text-2xl font-bold text-amber-900">{schedule.totalYAN.toFixed(1)}</p>
              </div>
              <div className="bg-amber-50 p-4 rounded">
                <p className="text-sm text-amber-700">Yeast Requirement</p>
                <p className="text-2xl font-bold text-amber-900">{schedule.yanCalculation.yeastRequirementName}</p>
              </div>
            </div>
          </div>

          {/* Nutrient Additions */}
          {schedule.additions.map((addition) => (
            <div key={addition.additionNumber} className="bg-white rounded-lg shadow-lg p-6">
              <div className="flex justify-between items-start mb-4">
                <div>
                  <h4 className="text-lg font-bold text-amber-900">
                    Addition {addition.additionNumber}: {addition.name}
                  </h4>
                  <p className="text-sm text-amber-700 mt-1">
                    <span className="font-semibold">{addition.timing}</span> - {addition.timingDetails}
                  </p>
                </div>
                <div className="text-right">
                  <p className="text-sm text-amber-700">YAN Added</p>
                  <p className="text-2xl font-bold text-amber-900">{addition.totalYAN.toFixed(1)} PPM</p>
                </div>
              </div>

              {/* Additives in this addition */}
              <div className="bg-amber-50 rounded p-4 mb-3">
                <p className="font-semibold text-amber-900 mb-3">Nutrient Additives:</p>
                <div className="space-y-2">
                  {addition.additives.map((nutrient, idx) => (
                    <div key={idx} className="flex justify-between items-center">
                      <div>
                        <p className="font-semibold text-amber-900">{nutrient.nutrientName}</p>
                        <p className="text-sm text-amber-700">{nutrient.yanContribution.toFixed(1)} PPM YAN</p>
                      </div>
                      <div className="text-right">
                        <p className="text-lg font-bold text-amber-900">{nutrient.amountGrams.toFixed(2)} g</p>
                      </div>
                    </div>
                  ))}
                </div>
              </div>

              {/* Notes */}
              {addition.notes && (
                <div className="bg-blue-50 border border-blue-200 rounded p-3">
                  <p className="text-sm text-blue-900">{addition.notes}</p>
                </div>
              )}
            </div>
          ))}

          {/* Totals Summary */}
          <div className="bg-gradient-to-r from-amber-100 to-yellow-100 rounded-lg shadow-lg p-6">
            <h3 className="text-lg font-bold text-amber-900 mb-4">Total Nutrient Amounts for {schedule.batchSizeLiters} Liters</h3>
            <div className="grid md:grid-cols-2 gap-4">
              {Object.entries(schedule.totalAdditives).map(([type, amount]) => {
                const typeNames = { '0': 'Go Ferm PE', '1': 'Fermaid K', '2': 'Fermaid O', '3': 'DAP' }
                return (
                  <div key={type} className="bg-white rounded p-4">
                    <p className="font-semibold text-amber-900">{typeNames[type]}</p>
                    <p className="text-2xl font-bold text-amber-900 mt-1">{amount.toFixed(2)} g</p>
                  </div>
                )
              })}
            </div>
          </div>
        </div>
      )}
    </div>
  )
}
