using MenuAPI.Models;

namespace MenuAPI.DTOs;
public class CreateDishDTO: IMenuAPICreateDTO<Dish>
{
    public required string Name { get; set; }

}