using MenuAPI.Models;
using MenuAPI.Services;
using Microsoft.AspNetCore.Mvc;

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
        var all = _ingredientService.GetAll();
        if ( _ingredientService.GetAll() != null )
        {
            return all.ToList();
        }
        else return NoContent();
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
    public IActionResult Create(Ingredient ingredient)
    {            
        _ingredientService.Add(ingredient);
        return CreatedAtAction(nameof(Get), new { id = ingredient.Id }, ingredient);
    }

    [HttpPut("{id}")]
    public IActionResult Update(long id, Ingredient ingredient)
    {
        if (id != ingredient.Id)
            return BadRequest();
            
        var existingIngredient = _ingredientService.Get(id);
        if(existingIngredient is null)
            return NotFound();
    
        _ingredientService.Update(ingredient);           
    
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        var ingredient = _ingredientService.Get(id);
    
        if (ingredient is null)
            return NotFound();
        
        _ingredientService.Delete(id);
    
        return NoContent();
    }

}