namespace MeadCalculator.API.Services;

using MeadCalculator.API.Models;

public interface ICalculationService
{
    CalculationResult Calculate(CalculationRequest request);
    double CalculateABV(double fermentableSugarsGrams, double volumeML);
    double CalculateGravity(double sugarsGrams, double volumeML);
}

public class CalculationService : ICalculationService
{
    // Fermentation efficiency constant
    // Typical yeast can ferment about 51% of available sugars to alcohol
    private const double FermentationEfficiency = 0.51;

    // Density of water at 20°C (g/ml)
    private const double WaterDensity = 1.0;

    public CalculationResult Calculate(CalculationRequest request)
    {
        var result = new CalculationResult();
        var ingredientBreakdowns = new List<IngredientBreakdown>();
        double totalSugarGrams = 0;
        double totalVolumeML = 0;
        double honeyWeightGrams = 0;

        // First pass: calculate sugars and volumes
        foreach (var ingredient in request.Ingredients)
        {
            double sugarGrams = 0;

            if (ingredient.Type == IngredientType.Honey)
            {
                // Honey is measured by weight
                sugarGrams = ingredient.Amount * (ingredient.SugarContentPercentage / 100);
                honeyWeightGrams = ingredient.Amount;
            }
            else if (ingredient.Type == IngredientType.Fruit)
            {
                // Fruits are measured by weight
                sugarGrams = ingredient.Amount * (ingredient.SugarContentPercentage / 100);
                // Approximate volume contribution (rough estimate)
                totalVolumeML += ingredient.Amount * 0.5; // ~0.5ml per gram for fruits
            }
            else if (ingredient.Type == IngredientType.FruitJuice)
            {
                // Fruit juices are measured by volume
                totalVolumeML += ingredient.Amount;
                sugarGrams = ingredient.Amount * (ingredient.SugarContentPercentage / 100);
            }
            else if (ingredient.Type == IngredientType.Water)
            {
                // Water is measured by volume (ml)
                totalVolumeML += ingredient.Amount;
            }

            totalSugarGrams += sugarGrams;

            ingredientBreakdowns.Add(new IngredientBreakdown
            {
                Name = ingredient.IngredientName,
                Amount = ingredient.Amount,
                Unit = ingredient.Unit,
                SugarGrams = sugarGrams,
                SugarPercentageOfTotal = 0 // Will be calculated after total is known
            });
        }

        // Calculate sugar percentages
        foreach (var breakdown in ingredientBreakdowns)
        {
            breakdown.SugarPercentageOfTotal = totalSugarGrams > 0
                ? (breakdown.SugarGrams / totalSugarGrams) * 100
                : 0;
        }

        // Calculate based on mode
        switch (request.Mode)
        {
            case CalculationMode.TargetABV:
                HandleTargetABVMode(result, request.TargetValue ?? 12, totalVolumeML, totalSugarGrams);
                break;
            case CalculationMode.TargetVolume:
                HandleTargetVolumeMode(result, request.TargetValue ?? 10000, totalVolumeML, totalSugarGrams);
                break;
            case CalculationMode.HoneyWeight:
                HandleHoneyWeightMode(result, totalVolumeML, totalSugarGrams);
                break;
        }

        result.TotalVolumeML = totalVolumeML;
        result.TotalFermentableSugarsGrams = totalSugarGrams;
        result.EstimatedABV = CalculateABV(totalSugarGrams, totalVolumeML);
        result.EstimatedOriginalGravity = CalculateGravity(totalSugarGrams, totalVolumeML);
        result.EstimatedFinalGravity = CalculateFinalGravity(result.EstimatedOriginalGravity, result.EstimatedABV);
        result.Ingredients = ingredientBreakdowns;

        return result;
    }

    public double CalculateABV(double fermentableSugarsGrams, double volumeML)
    {
        if (volumeML == 0) return 0;

        // Each gram of fermented sugar produces approximately 0.511 grams of ethanol
        // Ethanol has a lower specific gravity than water (~0.789)
        double fermentableSugars = fermentableSugarsGrams * FermentationEfficiency;
        double ethanolProduced = fermentableSugars * 0.511; // grams

        // ABV = (weight of ethanol / volume in ml) / density of ethanol * 100
        // Simplified: ABV = (ethanolGrams / volumeML) / 0.789 * 100
        double abv = (ethanolProduced / volumeML) / 0.789 * 100;

        return Math.Round(abv, 2);
    }

    public double CalculateGravity(double sugarsGrams, double volumeML)
    {
        if (volumeML == 0) return 1.0;

        // Original Gravity formula
        // OG = 1 + (sugar grams / volume in liters) / 1000 * specific gravity adjustment
        double sugarsDensity = sugarsGrams / (volumeML / 1000); // g/L
        double og = 1.0 + (sugarsDensity / 1000) * 0.04; // Simplified calculation

        return Math.Round(og, 4);
    }

    private double CalculateFinalGravity(double originalGravity, double abv)
    {
        // Approximate final gravity based on ABV
        // FG ≈ OG - (ABV * 0.00125)
        double fg = originalGravity - (abv * 0.00125);
        return Math.Round(Math.Max(fg, 0.990), 4); // FG should be at least 0.990
    }

    private void HandleTargetABVMode(CalculationResult result, double targetABV, double currentVolumeML, double currentSugarGrams)
    {
        if (currentVolumeML == 0) return;

        // Calculate required sugar grams for target ABV
        // Work backwards from desired ABV
        double requiredEthanol = targetABV * currentVolumeML * 0.789 / 100;
        double requiredSugars = requiredEthanol / (0.511 * FermentationEfficiency);

        double additionalSugarNeeded = requiredSugars - currentSugarGrams;

        // Convert to honey weight (honey is ~80% sugar)
        double honeyNeeded = additionalSugarNeeded / 0.80;

        result.CalculatedHoneyWeightGrams = Math.Round(Math.Max(0, honeyNeeded), 2);
        result.CalculatedABV = targetABV;
    }

    private void HandleTargetVolumeMode(CalculationResult result, double targetVolumeML, double currentVolumeML, double currentSugarGrams)
    {
        // Scale ingredient amounts proportionally to reach target volume
        if (currentVolumeML > 0)
        {
            double scaleFactor = targetVolumeML / currentVolumeML;
            result.CalculatedVolumeML = targetVolumeML;
        }
    }

    private void HandleHoneyWeightMode(CalculationResult result, double totalVolumeML, double totalSugarGrams)
    {
        // Already calculated in the main calculation
        result.CalculatedABV = CalculateABV(totalSugarGrams, totalVolumeML);
    }
}
