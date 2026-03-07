using MenuAPI.Models;

namespace MenuAPI.Services;
using Microsoft.EntityFrameworkCore;

public class IngredientService: IMenuService<Ingredient>
{   
    private List<Ingredient>? Ingredients{ get; }
    private MenuContext _menuContext { get; }

    public IngredientService(MenuContext menuContext)
    {
        _menuContext = menuContext;
        Ingredients = new List<Ingredient>();
        
    }
    public IEnumerable<Ingredient> GetAll()
    {
        return _menuContext.Ingredients;
    }

    public Ingredient? Get(long id) => Ingredients.FirstOrDefault(d => d.Id == id);

    public void Add(Ingredient ingredient)
    {
        ingredient.Id = MenuContext.IdGenerator.CreateId();
        Ingredients.Add(ingredient);
    }

    public void Delete(long id)
    {
        var ingredient = Get(id);
        if(ingredient is null)
            return;

        // TODO: cascading delete
        Ingredients.Remove(ingredient);
    }

    public void Update(Ingredient ingredient)
    {
        // TODO: change to use db
        var index = Ingredients.FindIndex(d => d.Id == ingredient.Id);
        if(index == -1)
            return;

        Ingredients[index] = ingredient;
    }

}