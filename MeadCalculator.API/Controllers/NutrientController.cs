namespace MeadCalculator.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using MeadCalculator.API.Models;
using MeadCalculator.API.Services;

[ApiController]
[Route("api/[controller]")]
public class NutrientController : ControllerBase
{
    private readonly INutrientService _nutrientService;

    public NutrientController(INutrientService nutrientService)
    {
        _nutrientService = nutrientService;
    }

    [HttpPost("calculate-yan")]
    public ActionResult<YanCalculationResult> CalculateYAN([FromBody] YanCalculationRequest request)
    {
        if (request == null)
            return BadRequest("Request cannot be null");

        try
        {
            var result = _nutrientService.CalculateYAN(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"YAN calculation error: {ex.Message}");
        }
    }

    [HttpPost("generate-sna")]
    public ActionResult<SnaNutrientSchedule> GenerateSNA([FromBody] SnaScheduleRequest request)
    {
        if (request == null)
            return BadRequest("Request cannot be null");

        if (request.BatchSizeLiters <= 0)
            return BadRequest("Batch size must be greater than 0");

        try
        {
            var schedule = _nutrientService.GenerateSnaSchedule(request);
            return Ok(schedule);
        }
        catch (Exception ex)
        {
            return BadRequest($"SNA generation error: {ex.Message}");
        }
    }

    [HttpGet("additives")]
    public ActionResult<List<NutrientAdditive>> GetAdditives()
    {
        try
        {
            var additives = _nutrientService.GetAvailableAdditives();
            return Ok(additives);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error retrieving additives: {ex.Message}");
        }
    }
}
