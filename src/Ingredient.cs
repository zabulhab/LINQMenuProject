using System.ComponentModel.DataAnnotations;

    // Represent most basic component of an IngredientList
    public class Ingredient
    {
        [Key] // f
        public int Id { get; set; }
        public string Name { get; set; } 

        // TODO: compile into enum?
        // public enum AllergenTypes
        // {
        //     AllergenDairy,
        //     AllergenGluten
        // }

        // These should correspond to the Allergens list in the MenuContext
        public AllergenStatusBool AllergenDairy { get; set; } = AllergenStatusBool.No;
        public AllergenStatusBool AllergenGluten { get; set; } = AllergenStatusBool.No;
        public AllergenStatusBool AllergenMeat { get; set; } = AllergenStatusBool.No;
        public AllergenStatusBool AllergenNut { get; set; } = AllergenStatusBool.No;
        public AllergenStatusBool AllergenSesame { get; set; } = AllergenStatusBool.No; 
        public AllergenStatusBool AllergenSpicy { get; set; } = AllergenStatusBool.No;
    }