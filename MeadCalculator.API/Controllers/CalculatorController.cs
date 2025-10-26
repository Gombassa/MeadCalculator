namespace MeadCalculator.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using MeadCalculator.API.Models;
using MeadCalculator.API.Services;

[ApiController]
[Route("api/[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly ICalculationService _calculationService;

    public CalculatorController(ICalculationService calculationService)
    {
        _calculationService = calculationService;
    }

    [HttpPost("calculate")]
    public ActionResult<CalculationResult> Calculate([FromBody] CalculationRequest request)
    {
        if (request == null || request.Ingredients.Count == 0)
            return BadRequest("Calculation request must contain at least one ingredient");

        try
        {
            var result = _calculationService.Calculate(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Calculation error: {ex.Message}");
        }
    }
}
