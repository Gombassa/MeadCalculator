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
            // Fruits - Alphabetical with flavor profiles
            new Ingredient
            {
                Id = 11,
                Name = "Apple (13% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 13,
                Unit = "g",
                Description = "Crisp, tart to sweet flavor. Adds bright acidity and subtle fruit notes with natural tannins"
            },
            new Ingredient
            {
                Id = 25,
                Name = "Apricots (9.15% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 9.15,
                Unit = "g",
                Description = "Stone fruit with peachy, aromatic flavor. Contributes floral notes and delicate fruit character"
            },
            new Ingredient
            {
                Id = 62,
                Name = "Barberries (5.0% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 5.0,
                Unit = "g",
                Description = "Very tart wild berry with lemony notes. Adds bright, sour punch and herbal undertones"
            },
            new Ingredient
            {
                Id = 30,
                Name = "Banana (13.9% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 13.9,
                Unit = "g",
                Description = "Tropical, creamy flavor with mild sweetness. Creates smooth mouthfeel and subtle tropical notes"
            },
            new Ingredient
            {
                Id = 15,
                Name = "Blackberry (10% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 10,
                Unit = "g",
                Description = "Deep berry flavor with slight tartness and earthy undertones. Rich, full-bodied contribution"
            },
            new Ingredient
            {
                Id = 12,
                Name = "Blueberry (10% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 10,
                Unit = "g",
                Description = "Mild, slightly sweet berry with subtle floral notes. Creates soft, jammy fruit character"
            },
            new Ingredient
            {
                Id = 27,
                Name = "Cantaloupe (8.35% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 8.35,
                Unit = "g",
                Description = "Sweet melon with peachy, musky aroma. Adds bright, summery fruit profile with honeyed notes"
            },
            new Ingredient
            {
                Id = 16,
                Name = "Cherry (16% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 16,
                Unit = "g",
                Description = "Rich, deep fruit flavor with subtle almond notes. Creates complex berry profile with natural tannins"
            },
            new Ingredient
            {
                Id = 49,
                Name = "Cloudberries (4.5% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 4.5,
                Unit = "g",
                Description = "Arctic berry with unique peachy-citrus flavor and herbal notes. Adds exotic, delicate character"
            },
            new Ingredient
            {
                Id = 56,
                Name = "Crab Apples (10.0% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 10.0,
                Unit = "g",
                Description = "Highly tart with sharp fruit notes. Adds significant acidity and wild, astringent character"
            },
            new Ingredient
            {
                Id = 60,
                Name = "Chokeberries/Aronia (4.5% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 4.5,
                Unit = "g",
                Description = "Intensely tart wild berry with earthy, tannic profile. Creates dry, puckering sensation"
            },
            new Ingredient
            {
                Id = 54,
                Name = "Elderberries (7.0% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 7.0,
                Unit = "g",
                Description = "Musky, earthy berry (must cook). Contributes deep, dark fruit with herbal, medicinal notes"
            },
            new Ingredient
            {
                Id = 26,
                Name = "Grapes (17.05% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 17.05,
                Unit = "g",
                Description = "Sweet, fruity with tannin structure. Creates wine-like complexity with natural fermentation character"
            },
            new Ingredient
            {
                Id = 52,
                Name = "Hawthorn Berries (6.0% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 6.0,
                Unit = "g",
                Description = "Tart wild berry with floral, slightly spicy notes. Adds herbal, astringent character"
            },
            new Ingredient
            {
                Id = 44,
                Name = "Huckleberries (6.5% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 6.5,
                Unit = "g",
                Description = "Wild berry with subtle earthiness and mild blueberry notes. Creates soft, rounded fruit profile"
            },
            new Ingredient
            {
                Id = 29,
                Name = "Honeydew Melon (8.2% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 8.2,
                Unit = "g",
                Description = "Sweet, delicate melon with floral notes. Adds smooth, honey-like sweetness and cooling effect"
            },
            new Ingredient
            {
                Id = 37,
                Name = "Kiwi (10.5% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 10.5,
                Unit = "g",
                Description = "Bright, tropical flavor with tart edge. Creates zingy, fresh fruit character with subtle acidity"
            },
            new Ingredient
            {
                Id = 45,
                Name = "Lingonberries (7.5% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 7.5,
                Unit = "g",
                Description = "Tart Nordic berry with cranberry-like sharpness. Adds bright acidity and herbal notes"
            },
            new Ingredient
            {
                Id = 58,
                Name = "Mountain Ash/Rowan (8.5% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 8.5,
                Unit = "g",
                Description = "Tart wild berry with astringent, spicy notes. Creates complex, warming, slightly floral profile"
            },
            new Ingredient
            {
                Id = 24,
                Name = "Nectarines (8.5% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 8.5,
                Unit = "g",
                Description = "Stone fruit with sweet, floral peachy flavor. Adds aromatic, juicy fruit character"
            },
            new Ingredient
            {
                Id = 18,
                Name = "Peach (9% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 9,
                Unit = "g",
                Description = "Delicate stone fruit with floral, peachy sweetness. Creates soft, summery fruit profile"
            },
            new Ingredient
            {
                Id = 19,
                Name = "Pear (11.95% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 11.95,
                Unit = "g",
                Description = "Delicate, slightly floral fruit flavor. Adds subtle pear notes with gentle sweetness"
            },
            new Ingredient
            {
                Id = 17,
                Name = "Plum (11% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 11,
                Unit = "g",
                Description = "Rich stone fruit with subtle spice and subtle tannins. Creates complex, slightly tart fruit profile"
            },
            new Ingredient
            {
                Id = 14,
                Name = "Raspberry (12% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 12,
                Unit = "g",
                Description = "Delicate berry with floral aromatics and subtle tartness. Adds elegant, refined fruit character"
            },
            new Ingredient
            {
                Id = 55,
                Name = "Rose Hips (8.7% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 8.7,
                Unit = "g",
                Description = "Tart wild fruit with tangy, citrus-like notes. Adds bright acidity and herbal, floral undertones"
            },
            new Ingredient
            {
                Id = 50,
                Name = "Sloe Berries (3.0% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 3.0,
                Unit = "g",
                Description = "Very tart wild berry with gin-like juniper notes. Creates sharp, peppery, intensely astringent profile"
            },
            new Ingredient
            {
                Id = 59,
                Name = "Sea Buckthorn (4.25% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 4.25,
                Unit = "g",
                Description = "Extreme tart with citrus and piney notes. Adds sharp acidity and high vitamin character"
            },
            new Ingredient
            {
                Id = 57,
                Name = "Serviceberries (11.5% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 11.5,
                Unit = "g",
                Description = "Wild berry with apple-like fruit notes and subtle spice. Creates balanced, fruity profile"
            },
            new Ingredient
            {
                Id = 23,
                Name = "Sour Cherries (8.1% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 8.1,
                Unit = "g",
                Description = "Tart cherry with sharp, fruity bite. Adds bright acidity and complex cherry flavor"
            },
            new Ingredient
            {
                Id = 22,
                Name = "Sweet Cherries (13.7% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 13.7,
                Unit = "g",
                Description = "Rich, deep sweet cherry flavor with mineral notes. Creates complex berry character"
            },
            new Ingredient
            {
                Id = 13,
                Name = "Strawberry (7% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 7,
                Unit = "g",
                Description = "Bright berry with delicate, fresh fruit flavor. Adds soft, sweet profile with subtle tartness"
            },
            new Ingredient
            {
                Id = 28,
                Name = "Watermelon (7.6% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 7.6,
                Unit = "g",
                Description = "Light, refreshing melon with subtle sweetness. Creates crisp, clean fruit profile"
            },
            new Ingredient
            {
                Id = 47,
                Name = "Wild Blackberries (5.25% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 5.25,
                Unit = "g",
                Description = "Deep wild berry with earthy tannins and slight tartness. Creates complex, structured flavor"
            },
            new Ingredient
            {
                Id = 43,
                Name = "Wild Blueberries (5.4% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 5.4,
                Unit = "g",
                Description = "Intense, concentrated wild blueberry with earthy notes. Creates rich, dark fruit profile"
            },
            new Ingredient
            {
                Id = 51,
                Name = "Wild Currants (6.0% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 6.0,
                Unit = "g",
                Description = "Tart berry with bright acidity and subtle spice. Adds sharp, complex fruit character"
            },
            new Ingredient
            {
                Id = 53,
                Name = "Wild Gooseberries (7.0% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 7.0,
                Unit = "g",
                Description = "Tart, herbaceous berry with slight floral notes. Creates bright, fresh, puckering profile"
            },
            new Ingredient
            {
                Id = 61,
                Name = "Wild Mulberries (10.0% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 10.0,
                Unit = "g",
                Description = "Wild berry with raspberry-like sweetness and subtle tartness. Creates soft, complex fruit"
            },
            new Ingredient
            {
                Id = 48,
                Name = "Wild Raspberries (6.5% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 6.5,
                Unit = "g",
                Description = "Delicate wild berry with floral, herbal notes. Adds elegant, refined profile with bright notes"
            },
            new Ingredient
            {
                Id = 46,
                Name = "Wild Strawberries (4.75% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 4.75,
                Unit = "g",
                Description = "Intensely fragrant wild berry with complex tartness. Creates aromatic, concentrated fruit character"
            },

            // Fruit Juices - Alphabetical with flavor profiles
            new Ingredient
            {
                Id = 31,
                Name = "Apple Juice (11.3% sugar)",
                Type = IngredientType.FruitJuice,
                SugarContentPercentage = 11.3,
                Unit = "ml",
                Description = "Bright, crisp apple flavor. Adds fresh, clean fruit profile with natural acidity"
            },
            new Ingredient
            {
                Id = 36,
                Name = "Blueberry Juice (12.0% sugar)",
                Type = IngredientType.FruitJuice,
                SugarContentPercentage = 12.0,
                Unit = "ml",
                Description = "Rich, concentrated blueberry flavor. Creates deep, jammy fruit character"
            },
            new Ingredient
            {
                Id = 33,
                Name = "Cranberry Juice (12.3% sugar)",
                Type = IngredientType.FruitJuice,
                SugarContentPercentage = 12.3,
                Unit = "ml",
                Description = "Tart, bright cranberry notes. Adds sharp acidity and slightly bitter undertones"
            },
            new Ingredient
            {
                Id = 34,
                Name = "Grape Juice (16.3% sugar)",
                Type = IngredientType.FruitJuice,
                SugarContentPercentage = 16.3,
                Unit = "ml",
                Description = "Sweet, full-bodied grape flavor. Creates wine-like complexity with natural tannins"
            },
            new Ingredient
            {
                Id = 32,
                Name = "Orange Juice (9.3% sugar)",
                Type = IngredientType.FruitJuice,
                SugarContentPercentage = 9.3,
                Unit = "ml",
                Description = "Bright citrus with sweet, tropical notes. Adds zesty, refreshing fruit character"
            },
            new Ingredient
            {
                Id = 35,
                Name = "Pomegranate Juice (13.3% sugar)",
                Type = IngredientType.FruitJuice,
                SugarContentPercentage = 13.3,
                Unit = "ml",
                Description = "Tart, complex berry with slight astringency. Adds deep, slightly spicy fruit notes"
            },

            // Honey - Alphabetical with flavor profiles
            new Ingredient
            {
                Id = 4,
                Name = "Buckwheat Honey (80% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 80,
                Unit = "g",
                Description = "Dark, robust flavor with molasses and earthy notes. Creates deep, complex mead with mineral character"
            },
            new Ingredient
            {
                Id = 1,
                Name = "Clover Honey (80% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 80,
                Unit = "g",
                Description = "Light, mild flavor with subtle floral notes. Creates smooth, delicate base with clean finish"
            },
            new Ingredient
            {
                Id = 3,
                Name = "Orange Blossom Honey (80% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 80,
                Unit = "g",
                Description = "Aromatic with distinct citrus and floral notes. Creates elegant, complex mead with bright character"
            },
            new Ingredient
            {
                Id = 2,
                Name = "Wildflower Honey (80% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 80,
                Unit = "g",
                Description = "Complex blend of floral flavors from mixed sources. Creates full-bodied mead with layered character"
            },

            // Water
            new Ingredient
            {
                Id = 41,
                Name = "Water (0% sugar)",
                Type = IngredientType.Water,
                SugarContentPercentage = 0,
                Unit = "ml",
                Description = "Pure water with no flavor contribution. Used to dilute and adjust mead volume"
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
