namespace MeadCalculator.API.Models;

public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Ingredients in the recipe
    public List<RecipeIngredient> RecipeIngredients { get; set; } = new();

    // Calculated values
    public double TotalVolumeML { get; set; }
    public double TotalFermentableSugarsGrams { get; set; }
    public double EstimatedABV { get; set; }
    public double EstimatedFinalGravity { get; set; }
    public double EstimatedOriginalGravity { get; set; }
}

public class RecipeIngredient
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }

    public int IngredientId { get; set; }
    public Ingredient Ingredient { get; set; }

    // Amount in the unit specified by the ingredient
    public double Amount { get; set; }

    // Calculated sugar contribution
    public double SugarGrams { get; set; }
}
