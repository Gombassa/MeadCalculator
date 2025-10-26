namespace MeadCalculator.API.Models;

public enum YeastNitrogenRequirement
{
    ExtraLow = 0,   // 0.5x multiplier
    Low = 1,        // 0.75x multiplier
    Medium = 2,     // 0.90x multiplier
    High = 3        // 1.25x multiplier
}

public enum NutrientType
{
    GoFermPE = 0,
    FermaidK = 1,
    FermaidO = 2,
    DAP = 3         // Diammonium Phosphate
}

public class NutrientAdditive
{
    public int Id { get; set; }
    public string Name { get; set; }
    public NutrientType Type { get; set; }
    public double NitrogenContentPercent { get; set; } // 1g/L adds X PPM YAN
    public double PpmPerGram { get; set; } // Convenience: NitrogenContentPercent * 100
    public string Description { get; set; }
    public bool IsOrganic { get; set; }
    public bool IsInorganic { get; set; }
    public double MaxCommercialLimit { get; set; } // in g/L, -1 if no limit
}

public class YanCalculationRequest
{
    public double SpecificGravity { get; set; }
    public double Brix { get; set; }
    public YeastNitrogenRequirement YeastRequirement { get; set; }
    public double ExistingYAN { get; set; } = 0; // PPM of existing nitrogen in must
}

public class YanCalculationResult
{
    public double TotalSugarPerLiter { get; set; }
    public double RequiredYAN { get; set; } // PPM
    public double YeastMultiplier { get; set; }
    public string YeastRequirementName { get; set; }
}

public class SnaNutrientSchedule
{
    public double BatchSizeGallons { get; set; }
    public YanCalculationResult YanCalculation { get; set; }
    public List<SnaNutrientAddition> Additions { get; set; } = new();
    public Dictionary<NutrientType, double> TotalAdditives { get; set; } = new();
    public double TotalYAN { get; set; }
}

public class SnaNutrientAddition
{
    public int AdditionNumber { get; set; }
    public string Name { get; set; }
    public string Timing { get; set; } // "At Rehydration", "24 hours post-pitch", etc.
    public string TimingDetails { get; set; }
    public List<NutrientAdditionDetail> Additives { get; set; } = new();
    public double TotalYAN { get; set; }
    public string Notes { get; set; }
}

public class NutrientAdditionDetail
{
    public NutrientType NutrientType { get; set; }
    public string NutrientName { get; set; }
    public double AmountGrams { get; set; }
    public double YanContribution { get; set; }
}

public class SnaScheduleRequest
{
    public double SpecificGravity { get; set; }
    public double Brix { get; set; }
    public YeastNitrogenRequirement YeastRequirement { get; set; }
    public double BatchSizeGallons { get; set; }
    public List<NutrientType> AdditivesToUse { get; set; } = new();
    public bool UseGoFerm { get; set; } = true;
    public bool UseFermaidO { get; set; } = true;
    public bool UseFermaidK { get; set; } = true;
    public bool UseDAP { get; set; } = true;
    public int NumberOfSteps { get; set; } = 3; // 2-4 additions typical
}
