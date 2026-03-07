using MenuAPI.Models;

namespace MenuAPI.Services;

public static class IngredientService
{   
    static List<Ingredient> Ingredients{ get; }
    static IngredientService()
    {
        Ingredients = new List<Ingredient>();
    }
    public static List<Ingredient> GetAll() => Ingredients;

    public static Ingredient? Get(long id) => Ingredients.FirstOrDefault(d => d.Id == id);

    public static void Add(Ingredient ingredient)
    {
        ingredient.Id = MenuContext.IdGenerator.CreateId();
        Ingredients.Add(ingredient);
    }

    public static void Delete(long id)
    {
        var ingredient = Get(id);
        if(ingredient is null)
            return;

        // TODO: cascading delete
        Ingredients.Remove(ingredient);
    }

    public static void Update(Ingredient ingredient)
    {
        // TODO: change to use db
        var index = Ingredients.FindIndex(d => d.Id == ingredient.Id);
        if(index == -1)
            return;

        Ingredients[index] = ingredient;
    }

}