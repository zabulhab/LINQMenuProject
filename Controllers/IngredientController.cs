using MenuAPI.Models;
using MenuAPI.Services;
using Microsoft.AspNetCore.Mvc;
using MenuAPI.DTOs;
using Microsoft.AspNetCore.JsonPatch.SystemTextJson;

namespace MenuAPI.Controllers;

[ApiController]
// [Route("Ingredient")]
[Route("[controller]")]
public class IngredientController : ControllerBase
{

    public IngredientController(MenuContext menuContext)
    { 
        _ingredientService = new IngredientService(menuContext);
    }
    private readonly IngredientService _ingredientService;

    [HttpGet]
    public ActionResult<IEnumerable<Ingredient>> GetAll()
    {
        return _ingredientService.GetAll();
    }


    [HttpGet("{id}")]
    public ActionResult<Ingredient> Get(long id)
    {
        var ingredient = _ingredientService.Get(id);

        if(ingredient == null)
            return NotFound();

        return ingredient;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateIngredientDTO dto)
    {            
        Ingredient? createdIngredient = _ingredientService.Add(dto);
        if ( createdIngredient == null )
        {
            return BadRequest("Ingredient could not be created.");
        }
        return Ok(createdIngredient);
    }

    [HttpPut("{id}")]
    public IActionResult Overwrite(long id, [FromBody] UpdateIngredientDTO dto)
    {
        if (id != dto.Id)
            return BadRequest();
            
        _ingredientService.Overwrite(dto);      
    
        return NoContent();
    }

    // Apply only requested changes, do not overwrite existing data
    [HttpPatch("{id}")]
    public IActionResult Update(long id, [FromBody] JsonPatchDocument<Ingredient> patchDoc)
    {
        Ingredient? updatedIngredient = _ingredientService.Update(id, patchDoc);
        return Ok(updatedIngredient);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        int result = _ingredientService.Delete(id);
    
        if (result == 1)
            return NotFound();
        
        _ingredientService.Delete(id);
    
        return NoContent();
    }

}