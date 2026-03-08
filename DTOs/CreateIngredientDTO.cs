namespace MenuAPI.DTOs;
public class CreateIngredientDTO: IMenuAPICreateDTO
{
    public required string Name { get; set; }

    // These should correspond to the Allergens list in the MenuContext
    public AllergenStatusBool? AllergenDairy { get; set; } = AllergenStatusBool.No;
    public AllergenStatusBool? AllergenGluten { get; set; } = AllergenStatusBool.No;
    public AllergenStatusBool? AllergenMeat { get; set; } = AllergenStatusBool.No;
    public AllergenStatusBool? AllergenNut { get; set; } = AllergenStatusBool.No;
    public AllergenStatusBool? AllergenSesame { get; set; } = AllergenStatusBool.No; 
    public AllergenStatusBool? AllergenSpicy { get; set; } = AllergenStatusBool.No;
}