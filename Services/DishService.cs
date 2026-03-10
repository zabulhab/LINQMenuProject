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

    public List<Ingredient> GetAllDishIngredients(long id)
    {
        List<Ingredient> ingredients = [];
        Dish? dish = _menuContext.Dishes.Find(id);
        if (dish == null)
        {
            Console.WriteLine($"Error: No Dish exists with Id {id}");
            return null!;
        }
        IQueryable<DishIngredientList> dishIngredientList = _menuContext.DishIngredientList.Where(d => d.Dish_ID == id);
        if (dishIngredientList.Count() != 1)
        {
            Console.WriteLine($"Error: No valid ingredient list was found for Dish with Id {id}");
            return null!;
        }
        IQueryable<DishIngredient> dishIngredients = _menuContext.DishIngredients.Where(
            d => d.DishIngredientList_ID == dishIngredientList.ElementAt(0).Id);
        if (!dishIngredients.Any())
        {
            Console.WriteLine($"Error: No valid ingredients were found for Dish with Id {id}");
            return null!;
        }
        foreach (DishIngredient di in dishIngredients)
        {
            Ingredient? ingredient = _menuContext.Ingredients.Find(di.Ingredient_ID);
            if (ingredient == null)
            {
                Console.WriteLine($"Error: Dish with Id {id} contained a non-existent ingredient");
                return null!;
            }
            ingredients.Add(ingredient!);
        }
        return ingredients;
    }

    public Dictionary<String, List<String>> 
        GetDishAllergens(long id)
    {
        Dictionary<String, List<String>>  allergensReturnMap = [];
        List<Ingredient> ingredients = GetAllDishIngredients(id);

        // Store pairs of each allergen found and the ingredient containing it
        List<(string, string)> presentAllergens = [];
        foreach (Ingredient i in ingredients)
        {
            Dictionary<string, AllergenStatusBool> allergensStatusMap = 
                i.allergensStatusMap;
            
            foreach (KeyValuePair<string, AllergenStatusBool> pair in allergensStatusMap)
            {
                if (pair.Value == AllergenStatusBool.Yes)
                {
                    presentAllergens.Add((pair.Key, i.Name));
                }
                else if (pair.Value == AllergenStatusBool.Maybe)
                {
                    presentAllergens.Add((pair.Key, i.Name+" (maybe)"));
                }
            }
        }

        // Condense allergens found into the return dictionary
        foreach ((string allergen, string ingredientName) entry in presentAllergens)
        {
            if (allergensReturnMap.ContainsKey(entry.allergen))
            {
                allergensReturnMap[entry.allergen].Add(entry.ingredientName);
            }
            else
            {
                allergensReturnMap.Add(entry.allergen, [entry.ingredientName]);
            }
        }

        return allergensReturnMap;
    }


}