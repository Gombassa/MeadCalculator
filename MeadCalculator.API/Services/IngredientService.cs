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
            },

            // Additional Farmed Fruits (Tree Fruits)
            new Ingredient
            {
                Id = 21,
                Name = "Pear (11.95% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 11.95,
                Unit = "g",
                Description = "Fresh pears - average sugar content 10.5-17.4g/100g"
            },
            new Ingredient
            {
                Id = 22,
                Name = "Sweet Cherries (13.7% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 13.7,
                Unit = "g",
                Description = "Fresh sweet cherries - average sugar content 12.8-14.6g/100g"
            },
            new Ingredient
            {
                Id = 23,
                Name = "Sour Cherries (8.1% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 8.1,
                Unit = "g",
                Description = "Fresh sour cherries - average sugar content 8.1g/100g"
            },
            new Ingredient
            {
                Id = 24,
                Name = "Nectarines (8.5% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 8.5,
                Unit = "g",
                Description = "Fresh nectarines - average sugar content 8.5g/100g"
            },
            new Ingredient
            {
                Id = 25,
                Name = "Apricots (9.15% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 9.15,
                Unit = "g",
                Description = "Fresh apricots - average sugar content 9.0-9.3g/100g"
            },
            new Ingredient
            {
                Id = 26,
                Name = "Grapes (17.05% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 17.05,
                Unit = "g",
                Description = "Fresh grapes - average sugar content 16.0-18.1g/100g"
            },
            new Ingredient
            {
                Id = 27,
                Name = "Cantaloupe (8.35% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 8.35,
                Unit = "g",
                Description = "Fresh cantaloupe - average sugar content 8.0-8.7g/100g"
            },
            new Ingredient
            {
                Id = 28,
                Name = "Watermelon (7.6% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 7.6,
                Unit = "g",
                Description = "Fresh watermelon - average sugar content 6.2-9.0g/100g"
            },
            new Ingredient
            {
                Id = 29,
                Name = "Honeydew Melon (8.2% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 8.2,
                Unit = "g",
                Description = "Fresh honeydew melon - average sugar content 8.2g/100g"
            },
            new Ingredient
            {
                Id = 30,
                Name = "Banana (13.9% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 13.9,
                Unit = "g",
                Description = "Fresh bananas - average sugar content 12.2-15.6g/100g"
            },
            new Ingredient
            {
                Id = 37,
                Name = "Kiwi (10.5% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 10.5,
                Unit = "g",
                Description = "Fresh kiwi fruit - average sugar content 10.5g/100g"
            },

            // Wild Berries
            new Ingredient
            {
                Id = 43,
                Name = "Wild Blueberries (5.4% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 5.4,
                Unit = "g",
                Description = "Wild blueberries/bilberries - average sugar content 4.0-6.8g/100g"
            },
            new Ingredient
            {
                Id = 44,
                Name = "Huckleberries (6.5% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 6.5,
                Unit = "g",
                Description = "Fresh huckleberries - average sugar content 5.0-8.0g/100g"
            },
            new Ingredient
            {
                Id = 45,
                Name = "Lingonberries (7.5% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 7.5,
                Unit = "g",
                Description = "Wild lingonberries/cowberries - average sugar content 5.0-10.0g/100g"
            },
            new Ingredient
            {
                Id = 46,
                Name = "Wild Strawberries (4.75% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 4.75,
                Unit = "g",
                Description = "Wild strawberries - average sugar content 3.5-6.0g/100g"
            },
            new Ingredient
            {
                Id = 47,
                Name = "Wild Blackberries (5.25% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 5.25,
                Unit = "g",
                Description = "Wild blackberries - average sugar content 4.0-6.5g/100g"
            },
            new Ingredient
            {
                Id = 48,
                Name = "Wild Raspberries (6.5% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 6.5,
                Unit = "g",
                Description = "Wild raspberries - average sugar content 5.0-8.0g/100g"
            },
            new Ingredient
            {
                Id = 49,
                Name = "Cloudberries (4.5% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 4.5,
                Unit = "g",
                Description = "Wild cloudberries - average sugar content 3.0-6.0g/100g"
            },
            new Ingredient
            {
                Id = 50,
                Name = "Sloe Berries (3.0% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 3.0,
                Unit = "g",
                Description = "Wild sloe berries - average sugar content 2.0-4.0g/100g"
            },
            new Ingredient
            {
                Id = 51,
                Name = "Wild Currants (6.0% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 6.0,
                Unit = "g",
                Description = "Wild currants (red/black) - average sugar content 4.0-8.0g/100g"
            },
            new Ingredient
            {
                Id = 52,
                Name = "Hawthorn Berries (6.0% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 6.0,
                Unit = "g",
                Description = "Wild hawthorn berries - average sugar content 4.0-8.0g/100g"
            },
            new Ingredient
            {
                Id = 53,
                Name = "Wild Gooseberries (7.0% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 7.0,
                Unit = "g",
                Description = "Wild gooseberries - average sugar content 5.0-9.0g/100g"
            },
            new Ingredient
            {
                Id = 54,
                Name = "Elderberries (7.0% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 7.0,
                Unit = "g",
                Description = "Black elderberries (must cook before eating) - average sugar content 7.0g/100g"
            },
            new Ingredient
            {
                Id = 55,
                Name = "Rose Hips (8.7% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 8.7,
                Unit = "g",
                Description = "Wild rose hips - average sugar content 2.4-15.0g/100g"
            },
            new Ingredient
            {
                Id = 56,
                Name = "Crab Apples (10.0% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 10.0,
                Unit = "g",
                Description = "Wild crab apples - average sugar content 8.0-12.0g/100g"
            },
            new Ingredient
            {
                Id = 57,
                Name = "Serviceberries (11.5% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 11.5,
                Unit = "g",
                Description = "Wild serviceberries/juneberries - average sugar content 8.0-15.0g/100g"
            },
            new Ingredient
            {
                Id = 58,
                Name = "Mountain Ash/Rowan (8.5% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 8.5,
                Unit = "g",
                Description = "Wild mountain ash/rowan berries - average sugar content 5.0-12.0g/100g"
            },
            new Ingredient
            {
                Id = 59,
                Name = "Sea Buckthorn (4.25% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 4.25,
                Unit = "g",
                Description = "Wild sea buckthorn - average sugar content 3.0-5.5g/100g"
            },
            new Ingredient
            {
                Id = 60,
                Name = "Chokeberries/Aronia (4.5% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 4.5,
                Unit = "g",
                Description = "Wild chokeberries/aronia - average sugar content 3.0-6.0g/100g"
            },
            new Ingredient
            {
                Id = 61,
                Name = "Wild Mulberries (10.0% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 10.0,
                Unit = "g",
                Description = "Wild mulberries - average sugar content 8.0-12.0g/100g"
            },
            new Ingredient
            {
                Id = 62,
                Name = "Barberries (5.0% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 5.0,
                Unit = "g",
                Description = "Wild barberries - average sugar content 3.0-7.0g/100g"
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
