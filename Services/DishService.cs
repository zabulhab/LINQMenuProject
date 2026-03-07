using MenuAPI.Models;

namespace MenuAPI.Services;

public static class DishService
{   
    static List<Dish> Dishes{ get; }
    static DishService()
    {
        Dishes = new List<Dish>();
    }
    public static List<Dish> GetAll() => Dishes;

    public static Dish? Get(long id) => Dishes.FirstOrDefault(d => d.Id == id);

    public static void Add(Dish dish)
    {
        dish.Id = MenuContext.IdGenerator.CreateId();
        Dishes.Add(dish);
    }

    public static void Delete(long id)
    {
        var dish = Get(id);
        if(dish is null)
            return;

        // TODO: cascading delete
        Dishes.Remove(dish);
    }

    public static void Update(Dish dish)
    {
        // TODO: change to use db
        var index = Dishes.FindIndex(d => d.Id == dish.Id);
        if(index == -1)
            return;

        Dishes[index] = dish;
    }

}