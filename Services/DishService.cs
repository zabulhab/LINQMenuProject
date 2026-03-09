using MenuAPI.Models;
using MenuAPI.DTOs;
using Microsoft.AspNetCore.JsonPatch.SystemTextJson;


namespace MenuAPI.Services;

public class DishService : IMenuService<Dish>
{
    private MenuContext _menuContext { get; }
    public DishService(MenuContext menuContext)
    {
        _menuContext = menuContext;
    }
    public List<Dish> GetAll()
    {
        return _menuContext.Dishes.ToList();
    }

    public Dish? Get(long id) => _menuContext.Dishes.FirstOrDefault(d => d.Id == id);

    public Dish Add(IMenuAPICreateDTO<Dish> dto)
    {
        CreateDishDTO d_dto;
        try { d_dto = (CreateDishDTO)dto; }
        catch
        {
            Console.WriteLine("Error: Cannot add Dish; controller passed in an unexpected format");
            return null!;
        }
        Dish dish = new Dish
        {
            Name = d_dto.Name
        };

        _menuContext.Dishes.Add(dish);
        _menuContext.SaveChanges();
        return dish;
    }

    public int Overwrite(IMenuAPIUpdateDTO<Dish> dto)
    {
        UpdateDishDTO d_dto;
        try { d_dto = (UpdateDishDTO)dto; }
        catch
        {
            Console.WriteLine("Error: Cannot update Dish; controller passed in an unexpected format");
            return 2;
        }
        Dish dish = new Dish
        {
            Id = d_dto.Id,
            Name = d_dto.Name ?? "",
        };

        Dish? existingDish = _menuContext.Dishes.Find(dish.Id);
        if (existingDish is null)
        {
            Console.WriteLine($"Cannot update; Dish with Id {dish.Id} does not exist on database");
            return 1;
        }
        _menuContext.Entry(existingDish).CurrentValues.SetValues(dish);
        _menuContext.SaveChanges();
        return 0;
    }

    public Dish Update(long id, JsonPatchDocument<Dish> patchDoc)
    {
        Dish? existingDish = _menuContext.Dishes.Find(id); // Fetch existing
        if (existingDish is null)
        {
            Console.WriteLine($"Cannot update; Dish with Id {id} does not exist on database");
            return null!;
        }
        // TODO: Return errors using ModelState, meaning make method return IActionResult...
        patchDoc.ApplyTo(existingDish); 
        _menuContext.SaveChanges();
        return existingDish;
    }

    public int Delete(long id)
    {
        Dish? existingDish = _menuContext.Dishes.Find(id);
        if (existingDish is null)
            return 1;

        // TODO: cascading delete?
        _menuContext.Dishes.Remove(existingDish);
        _menuContext.SaveChanges();
        return 0;
    }

}