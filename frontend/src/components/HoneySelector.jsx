export default function HoneySelector({
  honey,
  allHoneys,
  onUpdate,
}) {
  const selectedHoney = allHoneys.find(
    h => h.id === parseInt(honey.ingredientId)
  )

  const handleHoneyChange = (e) => {
    const ingredientId = parseInt(e.target.value)
    onUpdate('ingredientId', ingredientId)
  }

  const handleAmountChange = (e) => {
    onUpdate('amount', e.target.value)
  }

  return (
    <div className="bg-white rounded-lg shadow-lg p-6">
      <h2 className="text-2xl font-bold text-amber-900 mb-6">Honey Selection</h2>

      <div className="space-y-4">
        <div>
          <label htmlFor="honey-type" className="block text-sm font-semibold text-amber-900 mb-2">
            Honey Type <span className="text-red-600" aria-label="required">*</span>
          </label>
          <select
            id="honey-type"
            value={honey.ingredientId || ''}
            onChange={handleHoneyChange}
            aria-required="true"
            aria-label="Select honey type for your mead"
            className="w-full border border-amber-300 rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-amber-500 bg-white"
          >
            <option value="">Select a honey...</option>
            {allHoneys.map(h => (
              <option key={h.id} value={h.id}>
                {h.name} | {h.region || 'N/A'}
              </option>
            ))}
          </select>
        </div>

        {selectedHoney && (
          <div className="bg-amber-50 rounded p-4 border border-amber-200 space-y-3">
            <p className="text-sm text-amber-900">
              <span className="font-semibold">Median Sugar Content:</span> {selectedHoney.medianSugarContent ?? selectedHoney.sugarContentPercentage}% (used for calculations)
            </p>
            <p className="text-sm text-amber-900">
              <span className="font-semibold">Description:</span> {selectedHoney.description}
            </p>
          </div>
        )}

        <div>
          <label htmlFor="honey-amount" className="block text-sm font-semibold text-amber-900 mb-2">
            Amount ({selectedHoney?.unit || 'g'}) <span className="text-red-600" aria-label="required">*</span>
          </label>
          <input
            id="honey-amount"
            type="number"
            value={honey.amount}
            onChange={handleAmountChange}
            placeholder="e.g., 1000"
            step="0.1"
            aria-required="true"
            aria-label="Honey amount in grams"
            className="w-full border border-amber-300 rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-amber-500"
          />
        </div>
      </div>
    </div>
  )
}
