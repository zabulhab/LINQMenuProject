using MenuAPI.Models;
using MenuAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MenuAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class DishController : ControllerBase
{

    public DishController(){}

    [HttpGet]
    public ActionResult<List<Dish>> GetAll() =>
        DishService.GetAll();


    [HttpGet("{id}")]
    public ActionResult<Dish> Get(long id)
    {
        var dish = DishService.Get(id);

        if(dish == null)
            return NotFound();

        return dish;
    }

    [HttpPost]
    public IActionResult Create(Dish dish)
    {            
        DishService.Add(dish);
        return CreatedAtAction(nameof(Get), new { id = dish.Id }, dish);
    }

    [HttpPut("{id}")]
    public IActionResult Update(long id, Dish dish)
    {
        if (id != dish.Id)
            return BadRequest();
            
        var existingDish = DishService.Get(id);
        if(existingDish is null)
            return NotFound();
    
        DishService.Update(dish);           
    
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        var dish = DishService.Get(id);
    
        if (dish is null)
            return NotFound();
        
        DishService.Delete(id);
    
        return NoContent();
    }

}