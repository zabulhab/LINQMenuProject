using MenuAPI.Models;
using MenuAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MenuAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class IngredientController : ControllerBase
{

    public IngredientController(){}

    [HttpGet]
    public ActionResult<List<Ingredient>> GetAll() =>
        IngredientService.GetAll();


    [HttpGet("{id}")]
    public ActionResult<Ingredient> Get(long id)
    {
        var ingredient = IngredientService.Get(id);

        if(ingredient == null)
            return NotFound();

        return ingredient;
    }

    [HttpPost]
    public IActionResult Create(Ingredient ingredient)
    {            
        IngredientService.Add(ingredient);
        return CreatedAtAction(nameof(Get), new { id = ingredient.Id }, ingredient);
    }

    [HttpPut("{id}")]
    public IActionResult Update(long id, Ingredient ingredient)
    {
        if (id != ingredient.Id)
            return BadRequest();
            
        var existingIngredient = IngredientService.Get(id);
        if(existingIngredient is null)
            return NotFound();
    
        IngredientService.Update(ingredient);           
    
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        var ingredient = IngredientService.Get(id);
    
        if (ingredient is null)
            return NotFound();
        
        IngredientService.Delete(id);
    
        return NoContent();
    }

}