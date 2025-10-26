export default function IngredientForm({
  ingredient,
  allIngredients,
  onUpdate,
  onRemove,
}) {
  const selectedIngredient = allIngredients.find(
    ing => ing.id === parseInt(ingredient.ingredientId)
  )

  const handleIngredientChange = (e) => {
    const ingredientId = parseInt(e.target.value)
    onUpdate(ingredient.id, 'ingredientId', ingredientId)
  }

  const handleAmountChange = (e) => {
    onUpdate(ingredient.id, 'amount', e.target.value)
  }

  return (
    <div className="flex gap-4 items-end">
      <div className="flex-1">
        <label className="block text-sm font-semibold text-amber-900 mb-1">
          Ingredient
        </label>
        <select
          value={ingredient.ingredientId || ''}
          onChange={handleIngredientChange}
          className="w-full border border-amber-300 rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-amber-500"
        >
          <option value="">Select an ingredient...</option>
          {allIngredients.map(ing => (
            <option key={ing.id} value={ing.id}>
              {ing.name} - {ing.sugarContentPercentage}% sugar
            </option>
          ))}
        </select>
      </div>

      <div className="w-32">
        <label className="block text-sm font-semibold text-amber-900 mb-1">
          Amount
        </label>
        <input
          type="number"
          value={ingredient.amount}
          onChange={handleAmountChange}
          placeholder="0"
          step="0.1"
          className="w-full border border-amber-300 rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-amber-500"
        />
      </div>

      {selectedIngredient && (
        <div className="w-20">
          <label className="block text-sm font-semibold text-amber-900 mb-1">
            Unit
          </label>
          <div className="px-3 py-2 bg-amber-50 rounded border border-amber-200 text-sm">
            {selectedIngredient.unit}
          </div>
        </div>
      )}

      <button
        onClick={() => onRemove(ingredient.id)}
        className="bg-red-500 text-white px-3 py-2 rounded hover:bg-red-600 transition"
      >
        Remove
      </button>
    </div>
  )
}
