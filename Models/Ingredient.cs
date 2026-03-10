using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuAPI.Models;

// Represent most basic component of an IngredientList
public class Ingredient
{
    [Key,DatabaseGenerated(DatabaseGeneratedOption.None)] // f
    public long Id { get; set; }
    public string Name { get; set; } 

    // These should correspond to the Allergens list in the MenuContext
    
    //private AllergenStatusBool _allergenDairy = AllergenStatusBool.No;
    // public AllergenStatusBool AllergenDairy
    // { 
    //     get=>_allergenDairy;
    //     set
    //     { 
    //         _allergenDairy = value;
    //         bool exists = allergensStatusMap.ContainsKey("Dairy");
    //         if (exists == true)
    //         {
    //             allergensStatusMap["Dairy"] = value;
    //         }
    //         else 
    //         {
    //             allergensStatusMap.Add("Dairy", value);
    //         }    
    //     }
    // }

    public AllergenStatusBool AllergenDairy { get; set; } = AllergenStatusBool.No;
    public AllergenStatusBool AllergenGluten { get; set; } = AllergenStatusBool.No;
    public AllergenStatusBool AllergenMeat { get; set; } = AllergenStatusBool.No;
    public AllergenStatusBool AllergenNut { get; set; } = AllergenStatusBool.No;
    public AllergenStatusBool AllergenSesame { get; set; } = AllergenStatusBool.No; 
    public AllergenStatusBool AllergenSpicy { get; set; } = AllergenStatusBool.No;

    //private readonly Dictionary<string, AllergenStatusBool> _allergensStatusMap;
    [NotMapped]
    public Dictionary<string, AllergenStatusBool> allergensStatusMap 
    { 
        get
        {
            return new Dictionary<string, AllergenStatusBool>
            {
                {"Dairy", AllergenDairy},
                {"Gluten", AllergenGluten},
                {"Meat", AllergenMeat},
                {"Nut", AllergenNut},
                {"Sesame", AllergenSesame},
                {"Spicy", AllergenSpicy},
            };
        }
        set
        {
            // allergensStatusMap = new Dictionary<string, AllergenStatusBool>
            // {
            //     {"Dairy", AllergenDairy},
            //     {"Gluten", AllergenGluten},
            //     {"Meat", AllergenMeat},
            //     {"Nut", AllergenNut},
            //     {"Sesame", AllergenSesame},
            //     {"Spicy", AllergenSpicy},
            // };
        }
    }

    public Ingredient( string name = null )
    {
        Id = MenuContext.IdGenerator.CreateId();
        if (name != null) Name = name;
    }

}
