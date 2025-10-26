namespace MeadCalculator.API.Services;

using MeadCalculator.API.Models;

public interface IIngredientService
{
    List<Ingredient> GetAllIngredients();
    Ingredient GetIngredientById(int id);
    List<Ingredient> GetIngredientsByType(IngredientType type);
}

public class IngredientService : IIngredientService
{
    private static readonly List<Ingredient> Ingredients = InitializeIngredients();

    private static List<Ingredient> InitializeIngredients()
    {
        return new List<Ingredient>
        {
            // Honey
            new Ingredient
            {
                Id = 1,
                Name = "Clover Honey",
                Type = IngredientType.Honey,
                SugarContentPercentage = 80,
                Unit = "g",
                Description = "Light-colored, mild-flavored honey from clover flowers"
            },
            new Ingredient
            {
                Id = 2,
                Name = "Wildflower Honey",
                Type = IngredientType.Honey,
                SugarContentPercentage = 80,
                Unit = "g",
                Description = "Complex flavor from mixed wildflower sources"
            },
            new Ingredient
            {
                Id = 3,
                Name = "Orange Blossom Honey",
                Type = IngredientType.Honey,
                SugarContentPercentage = 80,
                Unit = "g",
                Description = "Aromatic honey with citrus notes"
            },
            new Ingredient
            {
                Id = 4,
                Name = "Buckwheat Honey",
                Type = IngredientType.Honey,
                SugarContentPercentage = 80,
                Unit = "g",
                Description = "Dark honey with robust, molasses-like flavor"
            },

            // Fruits (by weight)
            new Ingredient
            {
                Id = 11,
                Name = "Apple",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 13,
                Unit = "g",
                Description = "Fresh apples, varying sweetness by variety"
            },
            new Ingredient
            {
                Id = 12,
                Name = "Blueberry",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 10,
                Unit = "g",
                Description = "Fresh blueberries"
            },
            new Ingredient
            {
                Id = 13,
                Name = "Strawberry",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 7,
                Unit = "g",
                Description = "Fresh strawberries"
            },
            new Ingredient
            {
                Id = 14,
                Name = "Raspberry",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 12,
                Unit = "g",
                Description = "Fresh raspberries"
            },
            new Ingredient
            {
                Id = 15,
                Name = "Blackberry",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 10,
                Unit = "g",
                Description = "Fresh blackberries"
            },
            new Ingredient
            {
                Id = 16,
                Name = "Cherry",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 16,
                Unit = "g",
                Description = "Fresh cherries"
            },
            new Ingredient
            {
                Id = 17,
                Name = "Plum",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 11,
                Unit = "g",
                Description = "Fresh plums"
            },
            new Ingredient
            {
                Id = 18,
                Name = "Peach",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 9,
                Unit = "g",
                Description = "Fresh peaches"
            },
            new Ingredient
            {
                Id = 19,
                Name = "Pear",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 12,
                Unit = "g",
                Description = "Fresh pears"
            },
            new Ingredient
            {
                Id = 20,
                Name = "Raspberry",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 12,
                Unit = "g",
                Description = "Fresh raspberries"
            },

            // Fruit Juices (by volume)
            new Ingredient
            {
                Id = 31,
                Name = "Apple Juice",
                Type = IngredientType.FruitJuice,
                SugarContentPercentage = 11.3,
                Unit = "ml",
                Description = "Commercial apple juice"
            },
            new Ingredient
            {
                Id = 32,
                Name = "Orange Juice",
                Type = IngredientType.FruitJuice,
                SugarContentPercentage = 9.3,
                Unit = "ml",
                Description = "Commercial orange juice"
            },
            new Ingredient
            {
                Id = 33,
                Name = "Cranberry Juice",
                Type = IngredientType.FruitJuice,
                SugarContentPercentage = 12.3,
                Unit = "ml",
                Description = "Commercial cranberry juice"
            },
            new Ingredient
            {
                Id = 34,
                Name = "Grape Juice",
                Type = IngredientType.FruitJuice,
                SugarContentPercentage = 16.3,
                Unit = "ml",
                Description = "Commercial grape juice"
            },
            new Ingredient
            {
                Id = 35,
                Name = "Pomegranate Juice",
                Type = IngredientType.FruitJuice,
                SugarContentPercentage = 13.3,
                Unit = "ml",
                Description = "Commercial pomegranate juice"
            },
            new Ingredient
            {
                Id = 36,
                Name = "Blueberry Juice",
                Type = IngredientType.FruitJuice,
                SugarContentPercentage = 12.0,
                Unit = "ml",
                Description = "Commercial blueberry juice"
            },

            // Water
            new Ingredient
            {
                Id = 41,
                Name = "Water",
                Type = IngredientType.Water,
                SugarContentPercentage = 0,
                Unit = "ml",
                Description = "Pure water - no sugar content"
            },
            new Ingredient
            {
                Id = 42,
                Name = "Spring Water",
                Type = IngredientType.Water,
                SugarContentPercentage = 0,
                Unit = "ml",
                Description = "Natural spring water - no sugar content"
            }
        };
    }

    public List<Ingredient> GetAllIngredients()
    {
        return Ingredients;
    }

    public Ingredient GetIngredientById(int id)
    {
        return Ingredients.FirstOrDefault(i => i.Id == id);
    }

    public List<Ingredient> GetIngredientsByType(IngredientType type)
    {
        return Ingredients.Where(i => i.Type == type).ToList();
    }
}
