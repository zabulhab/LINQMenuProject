using MenuAPI.Models;

namespace MenuAPI.DTOs;
public class UpdateIngredientDTO: IMenuAPIUpdateDTO<Ingredient>
{
    public required long Id { get; set; }
    public string? Name { get; set; }

    // These should correspond to the Allergens list in the MenuContext
    public AllergenStatusBool? AllergenDairy { get; set; } = AllergenStatusBool.No;
    public AllergenStatusBool? AllergenGluten { get; set; } = AllergenStatusBool.No;
    public AllergenStatusBool? AllergenMeat { get; set; } = AllergenStatusBool.No;
    public AllergenStatusBool? AllergenNut { get; set; } = AllergenStatusBool.No;
    public AllergenStatusBool? AllergenSesame { get; set; } = AllergenStatusBool.No; 
    public AllergenStatusBool? AllergenSpicy { get; set; } = AllergenStatusBool.No;
}