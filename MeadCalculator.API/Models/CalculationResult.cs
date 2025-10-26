namespace MeadCalculator.API.Models;

public class CalculationResult
{
    public double TotalVolumeML { get; set; }
    public double TotalFermentableSugarsGrams { get; set; }
    public double EstimatedABV { get; set; }
    public double EstimatedOriginalGravity { get; set; }
    public double EstimatedFinalGravity { get; set; }

    // Ingredient breakdown
    public List<IngredientBreakdown> Ingredients { get; set; } = new();

    // Calculated adjustments based on mode
    public double? CalculatedHoneyWeightGrams { get; set; }
    public double? CalculatedVolumeML { get; set; }
    public double? CalculatedABV { get; set; }
}

public class IngredientBreakdown
{
    public string Name { get; set; }
    public double Amount { get; set; }
    public string Unit { get; set; }
    public double SugarGrams { get; set; }
    public double SugarPercentageOfTotal { get; set; }
}
