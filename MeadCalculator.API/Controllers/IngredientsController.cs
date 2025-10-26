namespace MeadCalculator.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using MeadCalculator.API.Models;
using MeadCalculator.API.Services;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
    private readonly IIngredientService _ingredientService;

    public IngredientsController(IIngredientService ingredientService)
    {
        _ingredientService = ingredientService;
    }

    [HttpGet]
    public ActionResult<List<Ingredient>> GetAllIngredients()
    {
        var ingredients = _ingredientService.GetAllIngredients();
        return Ok(ingredients);
    }

    [HttpGet("{id}")]
    public ActionResult<Ingredient> GetIngredient(int id)
    {
        var ingredient = _ingredientService.GetIngredientById(id);
        if (ingredient == null)
            return NotFound();

        return Ok(ingredient);
    }

    [HttpGet("by-type/{type}")]
    public ActionResult<List<Ingredient>> GetIngredientsByType(string type)
    {
        if (!Enum.TryParse<IngredientType>(type, true, out var ingredientType))
            return BadRequest("Invalid ingredient type");

        var ingredients = _ingredientService.GetIngredientsByType(ingredientType);
        return Ok(ingredients);
    }
}
