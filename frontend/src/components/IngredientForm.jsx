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

  const uniqueId = `ingredient-${ingredient.id}`

  return (
    <div className="flex gap-4 items-end">
      <div className="flex-1">
        <label htmlFor={`${uniqueId}-select`} className="block text-sm font-semibold text-amber-900 mb-1">
          Ingredient
        </label>
        <select
          id={`${uniqueId}-select`}
          value={ingredient.ingredientId || ''}
          onChange={handleIngredientChange}
          aria-label="Select additional ingredient"
          className="w-full border border-amber-300 rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-amber-500 bg-white"
        >
          <option value="">Select an ingredient...</option>
          {allIngredients.map(ing => (
            <option key={ing.id} value={ing.id}>
              {ing.name} | {ing.primarySugars} | {ing.region}
            </option>
          ))}
        </select>
      </div>

      <div className="w-32">
        <label htmlFor={`${uniqueId}-amount`} className="block text-sm font-semibold text-amber-900 mb-1">
          Amount
        </label>
        <input
          id={`${uniqueId}-amount`}
          type="number"
          value={ingredient.amount}
          onChange={handleAmountChange}
          placeholder="0"
          step="0.1"
          aria-label="Ingredient amount"
          className="w-full border border-amber-300 rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-amber-500"
        />
      </div>

      {selectedIngredient && (
        <div className="w-20">
          <label htmlFor={`${uniqueId}-unit`} className="block text-sm font-semibold text-amber-900 mb-1">
            Unit
          </label>
          <div id={`${uniqueId}-unit`} className="px-3 py-2 bg-amber-50 rounded border border-amber-200 text-sm">
            {selectedIngredient.unit}
          </div>
        </div>
      )}

      <button
        onClick={() => onRemove(ingredient.id)}
        aria-label={`Remove ${selectedIngredient?.name || 'ingredient'}`}
        title="Remove this ingredient"
        className="bg-red-500 text-white px-4 py-2 rounded hover:bg-red-600 transition min-h-[44px] min-w-[44px] flex items-center justify-center"
      >
        Remove
      </button>
    </div>
  )
}
