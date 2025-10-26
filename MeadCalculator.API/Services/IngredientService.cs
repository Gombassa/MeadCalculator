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
                Description = "Crisp, tart to sweet flavor. Adds bright acidity and subtle fruit notes with natural tannins",
                PrimarySugars = "Fructose (7.6g), Glucose (2.3g), Sucrose (3.3g)",
                Region = "North America, Europe, Britain"
            },
            new Ingredient
            {
                Id = 25,
                Name = "Apricots (9.15% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 9.15,
                Unit = "g",
                Description = "Stone fruit with peachy, aromatic flavor. Contributes floral notes and delicate fruit character",
                PrimarySugars = "Sucrose (5.2g), Glucose (1.6g), Fructose (0.7g)",
                Region = "North America, Europe"
            },
            new Ingredient
            {
                Id = 62,
                Name = "Barberries (5.0% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 5.0,
                Unit = "g",
                Description = "Very tart wild berry with lemony notes. Adds bright, sour punch and herbal undertones",
                PrimarySugars = "Very tart",
                Region = "Europe, North America"
            },
            new Ingredient
            {
                Id = 30,
                Name = "Banana (13.9% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 13.9,
                Unit = "g",
                Description = "Tropical, creamy flavor with mild sweetness. Creates smooth mouthfeel and subtle tropical notes",
                PrimarySugars = "Sucrose (6.5g), Glucose (4.2g), Fructose (2.7g)",
                Region = "Imported to all regions"
            },
            new Ingredient
            {
                Id = 15,
                Name = "Blackberry (10% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 10,
                Unit = "g",
                Description = "Deep berry flavor with slight tartness and earthy undertones. Rich, full-bodied contribution",
                PrimarySugars = "Fructose (4.1g), Glucose (3.1g), Sucrose (0.4g)",
                Region = "North America, Europe, Britain"
            },
            new Ingredient
            {
                Id = 12,
                Name = "Blueberry (10% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 10,
                Unit = "g",
                Description = "Mild, slightly sweet berry with subtle floral notes. Creates soft, jammy fruit character",
                PrimarySugars = "Fructose (3.6g), Glucose (3.5g), Sucrose (0.2g)",
                Region = "North America, Europe"
            },
            new Ingredient
            {
                Id = 27,
                Name = "Cantaloupe (8.35% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 8.35,
                Unit = "g",
                Description = "Sweet melon with peachy, musky aroma. Adds bright, summery fruit profile with honeyed notes",
                PrimarySugars = "Sucrose (5.4g), Fructose (1.8g), Glucose (1.2g)",
                Region = "North America"
            },
            new Ingredient
            {
                Id = 16,
                Name = "Cherry (16% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 16,
                Unit = "g",
                Description = "Rich, deep fruit flavor with subtle almond notes. Creates complex berry profile with natural tannins",
                PrimarySugars = "Glucose (8.1g), Fructose (6.2g), Sucrose (0.2g)",
                Region = "North America, Europe"
            },
            new Ingredient
            {
                Id = 49,
                Name = "Cloudberries (4.5% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 4.5,
                Unit = "g",
                Description = "Arctic berry with unique peachy-citrus flavor and herbal notes. Adds exotic, delicate character",
                PrimarySugars = "Glucose, Fructose",
                Region = "Northern Europe, Arctic regions"
            },
            new Ingredient
            {
                Id = 56,
                Name = "Crab Apples (10.0% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 10.0,
                Unit = "g",
                Description = "Highly tart with sharp fruit notes. Adds significant acidity and wild, astringent character",
                PrimarySugars = "Much lower than domestic apples",
                Region = "North America, Europe, Britain"
            },
            new Ingredient
            {
                Id = 60,
                Name = "Chokeberries/Aronia (4.5% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 4.5,
                Unit = "g",
                Description = "Intensely tart wild berry with earthy, tannic profile. Creates dry, puckering sensation",
                PrimarySugars = "Very tart",
                Region = "North America, Europe"
            },
            new Ingredient
            {
                Id = 54,
                Name = "Elderberries (7.0% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 7.0,
                Unit = "g",
                Description = "Musky, earthy berry (must cook). Contributes deep, dark fruit with herbal, medicinal notes",
                PrimarySugars = "Must be cooked before eating",
                Region = "North America, Europe, Britain"
            },
            new Ingredient
            {
                Id = 26,
                Name = "Grapes (17.05% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 17.05,
                Unit = "g",
                Description = "Sweet, fruity with tannin structure. Creates wine-like complexity with natural fermentation character",
                PrimarySugars = "Glucose (6.5g), Fructose (7.6g), Sucrose (0.1g)",
                Region = "North America, Europe"
            },
            new Ingredient
            {
                Id = 52,
                Name = "Hawthorn Berries (6.0% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 6.0,
                Unit = "g",
                Description = "Tart wild berry with floral, slightly spicy notes. Adds herbal, astringent character",
                PrimarySugars = "Variable by species",
                Region = "Europe, North America, Britain"
            },
            new Ingredient
            {
                Id = 44,
                Name = "Huckleberries (6.5% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 6.5,
                Unit = "g",
                Description = "Wild berry with subtle earthiness and mild blueberry notes. Creates soft, rounded fruit profile",
                PrimarySugars = "Variable by species",
                Region = "North America (Pacific Northwest)"
            },
            new Ingredient
            {
                Id = 29,
                Name = "Honeydew Melon (8.2% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 8.2,
                Unit = "g",
                Description = "Sweet, delicate melon with floral notes. Adds smooth, honey-like sweetness and cooling effect",
                PrimarySugars = "Mixed sugars",
                Region = "North America"
            },
            new Ingredient
            {
                Id = 37,
                Name = "Kiwi (10.5% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 10.5,
                Unit = "g",
                Description = "Bright, tropical flavor with tart edge. Creates zingy, fresh fruit character with subtle acidity",
                PrimarySugars = "Glucose (5.0g), Fructose (4.3g), Sucrose (1.1g)",
                Region = "North America, Europe"
            },
            new Ingredient
            {
                Id = 45,
                Name = "Lingonberries (7.5% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 7.5,
                Unit = "g",
                Description = "Tart Nordic berry with cranberry-like sharpness. Adds bright acidity and herbal notes",
                PrimarySugars = "Variable, higher acid content",
                Region = "Northern Europe, North America"
            },
            new Ingredient
            {
                Id = 58,
                Name = "Mountain Ash/Rowan (8.5% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 8.5,
                Unit = "g",
                Description = "Tart wild berry with astringent, spicy notes. Creates complex, warming, slightly floral profile",
                PrimarySugars = "Very tart when fresh",
                Region = "Northern Europe, Britain"
            },
            new Ingredient
            {
                Id = 24,
                Name = "Nectarines (8.5% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 8.5,
                Unit = "g",
                Description = "Stone fruit with sweet, floral peachy flavor. Adds aromatic, juicy fruit character",
                PrimarySugars = "Fructose (6.2g), Glucose (1.2g)",
                Region = "North America, Europe"
            },
            new Ingredient
            {
                Id = 18,
                Name = "Peach (9% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 9,
                Unit = "g",
                Description = "Delicate stone fruit with floral, peachy sweetness. Creates soft, summery fruit profile",
                PrimarySugars = "Fructose (1.3g), Sucrose (5.6g), Glucose (1.2g)",
                Region = "North America, Europe"
            },
            new Ingredient
            {
                Id = 19,
                Name = "Pear (11.95% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 11.95,
                Unit = "g",
                Description = "Delicate, slightly floral fruit flavor. Adds subtle pear notes with gentle sweetness",
                PrimarySugars = "Fructose (6.4g), Glucose (1.9g), Sucrose (1.8g)",
                Region = "North America, Europe, Britain"
            },
            new Ingredient
            {
                Id = 17,
                Name = "Plum (11% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 11,
                Unit = "g",
                Description = "Rich stone fruit with subtle spice and subtle tannins. Creates complex, slightly tart fruit profile",
                PrimarySugars = "Glucose (2.7g), Sucrose (3.0g), Fructose (1.8g)",
                Region = "North America, Europe, Britain"
            },
            new Ingredient
            {
                Id = 14,
                Name = "Raspberry (12% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 12,
                Unit = "g",
                Description = "Delicate berry with floral aromatics and subtle tartness. Adds elegant, refined fruit character",
                PrimarySugars = "Fructose (3.2g), Glucose (3.5g), Sucrose (2.8g)",
                Region = "North America, Europe, Britain"
            },
            new Ingredient
            {
                Id = 55,
                Name = "Rose Hips (8.7% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 8.7,
                Unit = "g",
                Description = "Tart wild fruit with tangy, citrus-like notes. Adds bright acidity and herbal, floral undertones",
                PrimarySugars = "Very variable, high S/A ratio",
                Region = "Europe, North America, Britain"
            },
            new Ingredient
            {
                Id = 50,
                Name = "Sloe Berries (3.0% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 3.0,
                Unit = "g",
                Description = "Very tart wild berry with gin-like juniper notes. Creates sharp, peppery, intensely astringent profile",
                PrimarySugars = "Very tart, low sugar",
                Region = "Europe, Britain"
            },
            new Ingredient
            {
                Id = 59,
                Name = "Sea Buckthorn (4.25% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 4.25,
                Unit = "g",
                Description = "Extreme tart with citrus and piney notes. Adds sharp acidity and high vitamin character",
                PrimarySugars = "Very tart, high vitamin C",
                Region = "Northern Europe"
            },
            new Ingredient
            {
                Id = 57,
                Name = "Serviceberries (11.5% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 11.5,
                Unit = "g",
                Description = "Wild berry with apple-like fruit notes and subtle spice. Creates balanced, fruity profile",
                PrimarySugars = "Higher sugar content",
                Region = "North America"
            },
            new Ingredient
            {
                Id = 23,
                Name = "Sour Cherries (8.1% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 8.1,
                Unit = "g",
                Description = "Tart cherry with sharp, fruity bite. Adds bright acidity and complex cherry flavor",
                PrimarySugars = "Glucose (4.2g), Fructose (3.3g), Sucrose (0.5g)",
                Region = "North America, Europe"
            },
            new Ingredient
            {
                Id = 22,
                Name = "Sweet Cherries (13.7% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 13.7,
                Unit = "g",
                Description = "Rich, deep sweet cherry flavor with mineral notes. Creates complex berry character",
                PrimarySugars = "Glucose (8.1g), Fructose (6.2g), Sucrose (0.2g)",
                Region = "North America, Europe"
            },
            new Ingredient
            {
                Id = 13,
                Name = "Strawberry (7% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 7,
                Unit = "g",
                Description = "Bright berry with delicate, fresh fruit flavor. Adds soft, sweet profile with subtle tartness",
                PrimarySugars = "Fructose (2.5g), Glucose (2.2g), Sucrose (1.0g)",
                Region = "North America, Europe, Britain"
            },
            new Ingredient
            {
                Id = 28,
                Name = "Watermelon (7.6% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 7.6,
                Unit = "g",
                Description = "Light, refreshing melon with subtle sweetness. Creates crisp, clean fruit profile",
                PrimarySugars = "Fructose (3.3g), Sucrose (3.6g), Glucose (1.6g)",
                Region = "North America"
            },
            new Ingredient
            {
                Id = 47,
                Name = "Wild Blackberries (5.25% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 5.25,
                Unit = "g",
                Description = "Deep wild berry with earthy tannins and slight tartness. Creates complex, structured flavor",
                PrimarySugars = "Lower than cultivated",
                Region = "North America, Europe, Britain"
            },
            new Ingredient
            {
                Id = 43,
                Name = "Wild Blueberries (5.4% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 5.4,
                Unit = "g",
                Description = "Intense, concentrated wild blueberry with earthy notes. Creates rich, dark fruit profile",
                PrimarySugars = "Fructose, Glucose",
                Region = "North America, Northern Europe"
            },
            new Ingredient
            {
                Id = 51,
                Name = "Wild Currants (6.0% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 6.0,
                Unit = "g",
                Description = "Tart berry with bright acidity and subtle spice. Adds sharp, complex fruit character",
                PrimarySugars = "Tart, variable",
                Region = "Europe, North America, Britain"
            },
            new Ingredient
            {
                Id = 53,
                Name = "Wild Gooseberries (7.0% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 7.0,
                Unit = "g",
                Description = "Tart, herbaceous berry with slight floral notes. Creates bright, fresh, puckering profile",
                PrimarySugars = "Variable by species",
                Region = "Europe, North America, Britain"
            },
            new Ingredient
            {
                Id = 61,
                Name = "Wild Mulberries (10.0% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 10.0,
                Unit = "g",
                Description = "Wild berry with raspberry-like sweetness and subtle tartness. Creates soft, complex fruit",
                PrimarySugars = "Variable by species",
                Region = "North America, Europe"
            },
            new Ingredient
            {
                Id = 48,
                Name = "Wild Raspberries (6.5% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 6.5,
                Unit = "g",
                Description = "Delicate wild berry with floral, herbal notes. Adds elegant, refined profile with bright notes",
                PrimarySugars = "Lower than cultivated",
                Region = "North America, Europe, Britain"
            },
            new Ingredient
            {
                Id = 46,
                Name = "Wild Strawberries (4.75% sugar)",
                Type = IngredientType.Fruit,
                SugarContentPercentage = 4.75,
                Unit = "g",
                Description = "Intensely fragrant wild berry with complex tartness. Creates aromatic, concentrated fruit character",
                PrimarySugars = "Much lower than cultivated",
                Region = "North America, Europe, Britain"
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
                Id = 101,
                Name = "Acacia Honey (75-78% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 76.5,
                Unit = "g",
                Description = "Sweet, delicate, mild floral with vanilla hints"
            },
            new Ingredient
            {
                Id = 102,
                Name = "Alfalfa Honey (74-76% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 75,
                Unit = "g",
                Description = "Mild, pleasingly sweet with beeswax aroma"
            },
            new Ingredient
            {
                Id = 103,
                Name = "Apple Honey (76-79% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 77.5,
                Unit = "g",
                Description = "Light, fruity, delicate apple notes"
            },
            new Ingredient
            {
                Id = 104,
                Name = "Avocado Honey (74-77% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 75.5,
                Unit = "g",
                Description = "Rich molasses, buttery, spicy aroma"
            },
            new Ingredient
            {
                Id = 105,
                Name = "Basswood/Linden Honey (74-76% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 75,
                Unit = "g",
                Description = "Fresh, woodsy, minty with slight bitterness"
            },
            new Ingredient
            {
                Id = 106,
                Name = "Blueberry Honey (75-78% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 76.5,
                Unit = "g",
                Description = "Full fruit flavor, flowery lemon aroma"
            },
            new Ingredient
            {
                Id = 4,
                Name = "Buckwheat Honey (73-76% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 74.5,
                Unit = "g",
                Description = "Strong, robust, molasses-like, slightly bitter"
            },
            new Ingredient
            {
                Id = 107,
                Name = "Cherry Honey (75-78% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 76.5,
                Unit = "g",
                Description = "Delicate, subtle fruit notes"
            },
            new Ingredient
            {
                Id = 108,
                Name = "Chestnut Honey (72-75% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 73.5,
                Unit = "g",
                Description = "Strong, slightly bitter, nutty"
            },
            new Ingredient
            {
                Id = 1,
                Name = "Clover Honey (76-78% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 77,
                Unit = "g",
                Description = "Sweet, floral, mild with slight sour aftertaste"
            },
            new Ingredient
            {
                Id = 109,
                Name = "Dandelion Honey (73-76% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 74.5,
                Unit = "g",
                Description = "Pronounced floral, tart finish"
            },
            new Ingredient
            {
                Id = 110,
                Name = "Eucalyptus Honey (75-78% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 76.5,
                Unit = "g",
                Description = "Herbal, menthol-like, medicinal scent"
            },
            new Ingredient
            {
                Id = 111,
                Name = "Fireweed Honey (74-77% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 75.5,
                Unit = "g",
                Description = "Buttery, smooth, delicate"
            },
            new Ingredient
            {
                Id = 112,
                Name = "Goldenrod Honey (74-77% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 75.5,
                Unit = "g",
                Description = "Rich, distinctive, slight bite, buttery"
            },
            new Ingredient
            {
                Id = 113,
                Name = "Heather Honey (73-76% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 74.5,
                Unit = "g",
                Description = "Very strong, distinctive, floral"
            },
            new Ingredient
            {
                Id = 114,
                Name = "Lavender Honey (76-79% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 77.5,
                Unit = "g",
                Description = "Delicate, subtle lavender taste and aroma"
            },
            new Ingredient
            {
                Id = 115,
                Name = "Lehua Honey (74-77% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 75.5,
                Unit = "g",
                Description = "Unique, tropical, distinctive"
            },
            new Ingredient
            {
                Id = 116,
                Name = "Linden (European) Honey (74-77% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 75.5,
                Unit = "g",
                Description = "Delicate, herbal aroma, hint of mint"
            },
            new Ingredient
            {
                Id = 117,
                Name = "Manuka Honey (72-78% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 75,
                Unit = "g",
                Description = "Rich, earthy, aromatic, slightly bitter"
            },
            new Ingredient
            {
                Id = 118,
                Name = "Mesquite Honey (74-77% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 75.5,
                Unit = "g",
                Description = "Delicate, distinctive desert characteristics"
            },
            new Ingredient
            {
                Id = 3,
                Name = "Orange Blossom Honey (75-77% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 76,
                Unit = "g",
                Description = "Light citrus, subtle fruity undertones"
            },
            new Ingredient
            {
                Id = 119,
                Name = "Palmetto Honey (75-78% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 76.5,
                Unit = "g",
                Description = "Distinctive, regional characteristics"
            },
            new Ingredient
            {
                Id = 120,
                Name = "Sage Honey (76-78% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 77,
                Unit = "g",
                Description = "Mild, delightful, herbal with sage aroma"
            },
            new Ingredient
            {
                Id = 121,
                Name = "Sourwood Honey (75-77% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 76,
                Unit = "g",
                Description = "Sweet, buttery, slightly spicy, caramel-like"
            },
            new Ingredient
            {
                Id = 122,
                Name = "Star Thistle Honey (76-79% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 77.5,
                Unit = "g",
                Description = "Delicate, hints of cinnamon and summer fruit"
            },
            new Ingredient
            {
                Id = 123,
                Name = "Tupelo Honey (76-79% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 77.5,
                Unit = "g",
                Description = "Mild, distinctive, buttery, very sweet"
            },
            new Ingredient
            {
                Id = 2,
                Name = "Wildflower Honey (73-76% sugar)",
                Type = IngredientType.Honey,
                SugarContentPercentage = 74.5,
                Unit = "g",
                Description = "Rich, fruity, robust (varies by region)"
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
