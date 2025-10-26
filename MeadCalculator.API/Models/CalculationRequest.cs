namespace MeadCalculator.API.Models;

public class CalculationRequest
{
    public List<CalculationIngredient> Ingredients { get; set; } = new();
    public CalculationMode Mode { get; set; }
    public double? TargetValue { get; set; } // ABV %, Volume (ml), or Honey weight (grams) depending on mode
}

public class CalculationIngredient
{
    public int IngredientId { get; set; }
    public string IngredientName { get; set; }
    public IngredientType Type { get; set; }
    public double Amount { get; set; }
    public double SugarContentPercentage { get; set; }
    public string Unit { get; set; }
}

public enum CalculationMode
{
    TargetABV,      // Calculate required honey for target ABV
    TargetVolume,   // Calculate ingredient ratios for target volume
    HoneyWeight     // Calculate final ABV based on honey weight
}
