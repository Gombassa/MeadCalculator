namespace MeadCalculator.API.Models;

public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IngredientType Type { get; set; }

    // Sugar content percentage (by weight for fruits, by volume for juices)
    public double SugarContentPercentage { get; set; }

    // Unit of measurement
    public string Unit { get; set; } // "g" for grams (fruits), "ml" for milliliters (juices)

    public string Description { get; set; }
}

public enum IngredientType
{
    Honey,
    Fruit,
    FruitJuice,
    Water,
    Yeast,
    Nutrient
}
