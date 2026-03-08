using MenuAPI.Models;
using MenuAPI.Services;
using Microsoft.AspNetCore.Mvc;
using MenuAPI.DTOs;

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
    private readonly IMenuService<Ingredient> _ingredientService;

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
    public IActionResult Update(long id, UpdateIngredientDTO dto)
    {
        if (id != dto.Id)
            return BadRequest();
            
        _ingredientService.Update(dto);      
    
        return NoContent();
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