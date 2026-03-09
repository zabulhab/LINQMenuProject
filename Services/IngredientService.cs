using MenuAPI.Models;
using MenuAPI.DTOs;
using Microsoft.AspNetCore.JsonPatch.SystemTextJson;

namespace MenuAPI.Services;

public class IngredientService: IMenuService<Ingredient>
{   
    private MenuContext _menuContext { get; }
    public IngredientService(MenuContext menuContext)
    {
        _menuContext = menuContext;
        
    }
    public List<Ingredient> GetAll()
    {
        return _menuContext.Ingredients.ToList();
    }

    public Ingredient? Get(long id) => _menuContext.Ingredients.FirstOrDefault(d => d.Id == id);

    public Ingredient Add(IMenuAPICreateDTO<Ingredient> dto)
    {
        CreateIngredientDTO ing_dto;
        try { ing_dto = (CreateIngredientDTO)dto; }
        catch 
        { 
            Console.WriteLine("Error: Cannot add Ingredient; controller passed in an unexpected format"); 
            return null!;
        }
        Ingredient ingredient = new Ingredient
        {
            Name = ing_dto.Name,
            AllergenDairy = (AllergenStatusBool)ing_dto.AllergenDairy!,
            AllergenGluten = (AllergenStatusBool)ing_dto.AllergenGluten!,
            AllergenMeat = (AllergenStatusBool)ing_dto.AllergenMeat!,
            AllergenNut = (AllergenStatusBool)ing_dto.AllergenNut!,
            AllergenSesame = (AllergenStatusBool)ing_dto.AllergenSesame!,
            AllergenSpicy = (AllergenStatusBool)ing_dto.AllergenSpicy!
        };
        
        _menuContext.Ingredients.Add(ingredient);
        _menuContext.SaveChanges();
        return ingredient;
    }

    public int Overwrite(IMenuAPIUpdateDTO<Ingredient> dto)
    {
        UpdateIngredientDTO ing_dto;
        try { ing_dto = (UpdateIngredientDTO)dto; }
        catch 
        { 
            Console.WriteLine("Error: Cannot update Ingredient; controller passed in an unexpected format"); 
            return 2;
        }

        Ingredient ingredient = new Ingredient
        {
            Id = ing_dto.Id,
            Name = ing_dto.Name?? "",
            AllergenDairy = (AllergenStatusBool)ing_dto.AllergenDairy!,
            AllergenGluten = (AllergenStatusBool)ing_dto.AllergenGluten!,
            AllergenMeat = (AllergenStatusBool)ing_dto.AllergenMeat!,
            AllergenNut = (AllergenStatusBool)ing_dto.AllergenNut!,
            AllergenSesame = (AllergenStatusBool)ing_dto.AllergenSesame!,
            AllergenSpicy = (AllergenStatusBool)ing_dto.AllergenSpicy!
        };

        Ingredient? existingIngredient = _menuContext.Ingredients.Find(ingredient.Id);
        if(existingIngredient is null)
        {
            Console.WriteLine($"Cannot update; Ingredient with Id {ingredient.Id} does not exist on database");
            return 1;            
        }
        _menuContext.Entry(existingIngredient).CurrentValues.SetValues(ingredient);
        _menuContext.SaveChanges();
        return 0;
    }

    public Ingredient Update(long id, JsonPatchDocument<Ingredient> patchDoc)
    {
        Ingredient? existingIngredient = _menuContext.Ingredients.Find(id);
        if(existingIngredient is null)
        {
            Console.WriteLine($"Cannot update; Ingredient with Id {id} does not exist on database");
            return null!;           
        }
        // TODO: Return errors using ModelState, meaning make method return IActionResult...
        patchDoc.ApplyTo(existingIngredient);
        _menuContext.SaveChanges();
        return existingIngredient;
    }

    public int Delete(long id)
    {
        Ingredient? existingIngredient = _menuContext.Ingredients.Find(id);
        if(existingIngredient is null)
            return 1;

        // TODO: cascading delete?
        _menuContext.Ingredients.Remove(existingIngredient);
        _menuContext.SaveChanges();
        return 0;
    }

}