namespace MeadCalculator.API.Services;

using MeadCalculator.API.Models;

public interface INutrientService
{
    YanCalculationResult CalculateYAN(YanCalculationRequest request);
    SnaNutrientSchedule GenerateSnaSchedule(SnaScheduleRequest request);
    List<NutrientAdditive> GetAvailableAdditives();
}

public class NutrientService : INutrientService
{
    private static readonly List<NutrientAdditive> AvailableAdditives = InitializeAdditives();

    private static List<NutrientAdditive> InitializeAdditives()
    {
        return new List<NutrientAdditive>
        {
            new NutrientAdditive
            {
                Id = 1,
                Name = "Go Ferm PE",
                Type = NutrientType.GoFermPE,
                NitrogenContentPercent = 3,
                PpmPerGram = 30,
                Description = "Micronutrients and organic nitrogen for yeast rehydration",
                IsOrganic = true,
                IsInorganic = false,
                MaxCommercialLimit = -1
            },
            new NutrientAdditive
            {
                Id = 2,
                Name = "Fermaid O",
                Type = NutrientType.FermaidO,
                NitrogenContentPercent = 4,
                PpmPerGram = 40,
                Description = "Organic blend of micronutrients and nitrogen (OMRI Certified)",
                IsOrganic = true,
                IsInorganic = false,
                MaxCommercialLimit = -1 // No TTB limit
            },
            new NutrientAdditive
            {
                Id = 3,
                Name = "Fermaid K",
                Type = NutrientType.FermaidK,
                NitrogenContentPercent = 10,
                PpmPerGram = 100,
                Description = "Blend of micronutrients, organic and inorganic nitrogen",
                IsOrganic = true,
                IsInorganic = true,
                MaxCommercialLimit = 0.5 // 500 mg/L = 0.5 g/L
            },
            new NutrientAdditive
            {
                Id = 4,
                Name = "DAP (Diammonium Phosphate)",
                Type = NutrientType.DAP,
                NitrogenContentPercent = 21,
                PpmPerGram = 210,
                Description = "Dense source of inorganic nitrogen only",
                IsOrganic = false,
                IsInorganic = true,
                MaxCommercialLimit = -1
            }
        };
    }

    public List<NutrientAdditive> GetAvailableAdditives()
    {
        return AvailableAdditives;
    }

    public YanCalculationResult CalculateYAN(YanCalculationRequest request)
    {
        // Formula: Required PPM N = Brix * Specific Gravity * 10 * [yeast multiplier]
        // Or simplified: Sugar (g/L) = Brix * Specific Gravity * 10

        var sugarPerLiter = request.Brix * request.SpecificGravity * 10;

        var multiplier = request.YeastRequirement switch
        {
            YeastNitrogenRequirement.ExtraLow => 0.5,
            YeastNitrogenRequirement.Low => 0.75,
            YeastNitrogenRequirement.Medium => 0.90,
            YeastNitrogenRequirement.High => 1.25,
            _ => 0.75
        };

        var requiredYan = sugarPerLiter * multiplier;

        // Account for existing nitrogen
        var totalRequired = Math.Max(0, requiredYan - request.ExistingYAN);

        return new YanCalculationResult
        {
            TotalSugarPerLiter = sugarPerLiter,
            RequiredYAN = totalRequired,
            YeastMultiplier = multiplier,
            YeastRequirementName = request.YeastRequirement.ToString()
        };
    }

    public SnaNutrientSchedule GenerateSnaSchedule(SnaScheduleRequest request)
    {
        // Calculate required YAN
        var yanRequest = new YanCalculationRequest
        {
            SpecificGravity = request.SpecificGravity,
            Brix = request.Brix,
            YeastRequirement = request.YeastRequirement,
            ExistingYAN = 0
        };

        var yanCalc = CalculateYAN(yanRequest);
        var batchSizeLiters = request.BatchSizeGallons * 3.785; // Convert gallons to liters

        var schedule = new SnaNutrientSchedule
        {
            BatchSizeGallons = request.BatchSizeGallons,
            YanCalculation = yanCalc
        };

        // Build SNA schedule based on additives selected
        var totalYanNeeded = yanCalc.RequiredYAN;
        var organicYanNeeded = totalYanNeeded * 0.7; // 70% organic, 30% inorganic
        var inorganicYanNeeded = totalYanNeeded * 0.3;

        // Addition 1: Go Ferm PE (at rehydration)
        if (request.UseGoFerm)
        {
            var goFermAddition = new SnaNutrientAddition
            {
                AdditionNumber = 1,
                Name = "Rehydration",
                Timing = "At Rehydration",
                TimingDetails = "Rehydrate dry yeast with Go Ferm PE just prior to pitch (1.25g GoFerm per 1g yeast)",
                Notes = "Typical pitch rate is 2g yeast per gallon. Standard addition ~10g GoFerm per gallon."
            };

            var goFermGramsPerLiter = 2.64; // ~10g per gallon = 2.64g/L
            var goFermGramsByVolume = goFermGramsPerLiter * batchSizeLiters;
            var goFermYan = goFermGramsByVolume * 0.30; // 30 PPM per gram

            goFermAddition.Additives.Add(new NutrientAdditionDetail
            {
                NutrientType = NutrientType.GoFermPE,
                NutrientName = "Go Ferm PE",
                AmountGrams = goFermGramsByVolume,
                YanContribution = goFermYan
            });

            goFermAddition.TotalYAN = goFermYan;
            schedule.Additions.Add(goFermAddition);
            schedule.TotalAdditives[NutrientType.GoFermPE] = goFermGramsByVolume;
            schedule.TotalYAN += goFermYan;
        }

        // Addition 2: Fermaid O (post-lag, 24 hours after pitch)
        if (request.UseFermaidO)
        {
            var fermaidOAddition = new SnaNutrientAddition
            {
                AdditionNumber = 2,
                Name = "Post-Lag Organic Nitrogen",
                Timing = "24 hours after pitch",
                TimingDetails = "Add Fermaid O at first signs of active fermentation"
            };

            // Use 70% of organic nitrogen needs with Fermaid O
            var fermaidONeeded = organicYanNeeded;
            var fermaidOGrams = fermaidONeeded / 40; // 40 PPM per gram
            var fermaidOGramsByVolume = fermaidOGrams * batchSizeLiters;

            fermaidOAddition.Additives.Add(new NutrientAdditionDetail
            {
                NutrientType = NutrientType.FermaidO,
                NutrientName = "Fermaid O",
                AmountGrams = fermaidOGramsByVolume,
                YanContribution = fermaidONeeded
            });

            fermaidOAddition.TotalYAN = fermaidONeeded;
            fermaidOAddition.Notes = "Organic nitrogen is better assimilated by yeast. Can be split into 2 additions 24 hours apart if preferred.";
            schedule.Additions.Add(fermaidOAddition);
            schedule.TotalAdditives[NutrientType.FermaidO] = fermaidOGramsByVolume;
            schedule.TotalYAN += fermaidONeeded;
        }

        // Additions 3+: DAP and Fermaid K (staggered every 24 hours)
        var stepsRemaining = Math.Max(2, request.NumberOfSteps - 2);
        var inorganicPerStep = inorganicYanNeeded / stepsRemaining;

        for (int i = 0; i < stepsRemaining; i++)
        {
            var stepNumber = 3 + i;
            var daysAfterPitch = 2 + (i * 1); // Start 2 days after pitch, then every 24 hours

            var stepAddition = new SnaNutrientAddition
            {
                AdditionNumber = stepNumber,
                Name = $"Inorganic Nitrogen Step {i + 1}",
                Timing = $"{daysAfterPitch} days after pitch",
                TimingDetails = $"Add at 24 hour intervals, before reaching 9% ABV"
            };

            // Mix DAP and Fermaid K
            var useDAP = request.UseDAP && i < stepsRemaining - 1; // Use DAP for first steps
            var useFermaidK = request.UseFermaidK;

            if (useDAP)
            {
                var dapGrams = (inorganicPerStep / 210) * batchSizeLiters; // 210 PPM per gram
                stepAddition.Additives.Add(new NutrientAdditionDetail
                {
                    NutrientType = NutrientType.DAP,
                    NutrientName = "DAP",
                    AmountGrams = dapGrams,
                    YanContribution = inorganicPerStep
                });

                schedule.TotalAdditives.TryGetValue(NutrientType.DAP, out var currentDAP);
                schedule.TotalAdditives[NutrientType.DAP] = currentDAP + dapGrams;
            }
            else if (useFermaidK)
            {
                var fermaidKGrams = (inorganicPerStep / 100) * batchSizeLiters; // 100 PPM per gram
                stepAddition.Additives.Add(new NutrientAdditionDetail
                {
                    NutrientType = NutrientType.FermaidK,
                    NutrientName = "Fermaid K",
                    AmountGrams = fermaidKGrams,
                    YanContribution = inorganicPerStep
                });

                schedule.TotalAdditives.TryGetValue(NutrientType.FermaidK, out var currentFK);
                schedule.TotalAdditives[NutrientType.FermaidK] = currentFK + fermaidKGrams;
            }

            stepAddition.TotalYAN = inorganicPerStep;
            stepAddition.Notes = i == stepsRemaining - 1
                ? "This is the final inorganic nitrogen addition - keep it before 9% ABV is reached"
                : "Inorganic nitrogen must be added before yeast reaches 9% ABV";

            schedule.Additions.Add(stepAddition);
            schedule.TotalYAN += inorganicPerStep;
        }

        return schedule;
    }
}
