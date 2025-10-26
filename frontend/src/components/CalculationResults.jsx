export default function CalculationResults({ results }) {
  return (
    <div className="bg-gradient-to-br from-amber-50 to-yellow-50 rounded-lg shadow-lg p-6 sticky top-20">
      <h2 className="text-2xl font-bold text-amber-900 mb-6">Results</h2>

      {/* Key Metrics */}
      <div className="space-y-4 mb-6">
        <div className="bg-white rounded p-4 border-l-4 border-amber-600">
          <p className="text-sm text-amber-600 font-semibold">Estimated ABV</p>
          <p className="text-3xl font-bold text-amber-900">{results.estimatedABV}%</p>
        </div>

        <div className="bg-white rounded p-4 border-l-4 border-yellow-600">
          <p className="text-sm text-yellow-600 font-semibold">Total Volume</p>
          <p className="text-3xl font-bold text-amber-900">{results.totalVolumeML.toFixed(0)} ml</p>
        </div>

        <div className="bg-white rounded p-4 border-l-4 border-orange-600">
          <p className="text-sm text-orange-600 font-semibold">Total Fermentable Sugars</p>
          <p className="text-2xl font-bold text-amber-900">{results.totalFermentableSugarsGrams.toFixed(2)} g</p>
        </div>
      </div>

      {/* Gravity Information */}
      <div className="bg-white rounded p-4 mb-6 border border-amber-200">
        <h3 className="font-semibold text-amber-900 mb-3">Gravity</h3>
        <div className="space-y-2 text-sm">
          <div className="flex justify-between">
            <span className="text-amber-700">Original Gravity (OG)</span>
            <span className="font-semibold text-amber-900">{results.estimatedOriginalGravity.toFixed(4)}</span>
          </div>
          <div className="flex justify-between">
            <span className="text-amber-700">Final Gravity (FG)</span>
            <span className="font-semibold text-amber-900">{results.estimatedFinalGravity.toFixed(4)}</span>
          </div>
        </div>
      </div>

      {/* Calculated Values */}
      {(results.calculatedHoneyWeightGrams ||
        results.calculatedVolumeML ||
        results.calculatedABV) && (
        <div className="bg-blue-50 rounded p-4 mb-6 border border-blue-200">
          <h3 className="font-semibold text-blue-900 mb-3">Calculated Values</h3>
          <div className="space-y-2 text-sm">
            {results.calculatedHoneyWeightGrams && (
              <div className="flex justify-between">
                <span className="text-blue-700">Honey Needed</span>
                <span className="font-semibold text-blue-900">
                  {results.calculatedHoneyWeightGrams.toFixed(0)} g
                </span>
              </div>
            )}
            {results.calculatedVolumeML && (
              <div className="flex justify-between">
                <span className="text-blue-700">Target Volume</span>
                <span className="font-semibold text-blue-900">
                  {results.calculatedVolumeML.toFixed(0)} ml
                </span>
              </div>
            )}
            {results.calculatedABV && (
              <div className="flex justify-between">
                <span className="text-blue-700">Target ABV</span>
                <span className="font-semibold text-blue-900">
                  {results.calculatedABV.toFixed(2)}%
                </span>
              </div>
            )}
          </div>
        </div>
      )}

      {/* Ingredient Breakdown */}
      <div className="border-t-2 border-amber-200 pt-4">
        <h3 className="font-semibold text-amber-900 mb-3">Ingredient Breakdown</h3>
        <div className="space-y-2 text-xs">
          {results.ingredients.map((ing, idx) => (
            <div key={idx} className="bg-white p-2 rounded border border-amber-100">
              <div className="flex justify-between mb-1">
                <span className="font-semibold text-amber-900">{ing.name}</span>
                <span className="text-amber-700">{ing.amount} {ing.unit}</span>
              </div>
              <div className="flex justify-between text-amber-700">
                <span>Sugar: {ing.sugarGrams.toFixed(1)} g</span>
                <span>{ing.sugarPercentageOfTotal.toFixed(1)}%</span>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  )
}
