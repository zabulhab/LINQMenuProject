using MenuAPI.Models;
using MenuAPI.Services;
using Microsoft.AspNetCore.Mvc;
using MenuAPI.DTOs;
using Microsoft.AspNetCore.JsonPatch.SystemTextJson;

namespace MenuAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class DishController : ControllerBase
{

    public DishController(MenuContext menuContext)
    { 
        _dishService = new DishService(menuContext);
    }
    private readonly IMenuService<Dish> _dishService;

    [HttpGet]
    public ActionResult<IEnumerable<Dish>> GetAll()
    {
        return _dishService.GetAll();
    }


    [HttpGet("{id}")]
    public ActionResult<Dish> Get(long id)
    {
        var dish = _dishService.Get(id);

        if(dish == null)
            return NotFound();

        return dish;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateDishDTO dto)
    {            
        Dish? createdDish = _dishService.Add(dto);
        if ( createdDish == null )
        {
            return BadRequest("Dish could not be created.");
        }
        return Ok(createdDish);
    }

    [HttpPut("{id}")]
    public IActionResult Overwrite(long id, [FromBody] UpdateDishDTO dto)
    {
        if (id != dto.Id)
            return BadRequest();
            
        _dishService.Overwrite(dto);      
    
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult Update(long id, [FromBody] JsonPatchDocument<Dish> patchDoc)
    {
        Dish? updatedDish = _dishService.Update(id, patchDoc);
        return Ok(updatedDish);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        int result = _dishService.Delete(id);
    
        if (result == 1)
            return NotFound();
        
        _dishService.Delete(id);
    
        return NoContent();
    }

}