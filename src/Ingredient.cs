using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

    // Represent most basic component of an IngredientList
    public class Ingredient
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)] // f
        public long Id { get; private set; }
        public string Name { get; set; } 

        // These should correspond to the Allergens list in the MenuContext
        public AllergenStatusBool AllergenDairy { get; set; } = AllergenStatusBool.No;
        public AllergenStatusBool AllergenGluten { get; set; } = AllergenStatusBool.No;
        public AllergenStatusBool AllergenMeat { get; set; } = AllergenStatusBool.No;
        public AllergenStatusBool AllergenNut { get; set; } = AllergenStatusBool.No;
        public AllergenStatusBool AllergenSesame { get; set; } = AllergenStatusBool.No; 
        public AllergenStatusBool AllergenSpicy { get; set; } = AllergenStatusBool.No;
 
        public Ingredient( string name = null )
        {
            Id = EFTest.MenuContext.IdGenerator.CreateId();
            if (name != null) Name = name;
        }

    }