using MenuAPI.Models;

namespace MenuAPI.DTOs;
public class UpdateDishDTO: IMenuAPIUpdateDTO<Dish>
{
    public required long Id { get; set; }
    public string? Name { get; set; }

    // TODO: add ways to update/add/delete ingredients
    
}