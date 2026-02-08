using System.ComponentModel.DataAnnotations;

    // Represent most basic component of an IngredientList
    public class Ingredient
    {
        [Key] // f
        public int Id { get; set; }
        public string Name { get; set; } 

        public AllergenStatusBool AllergenMeat { get; set; } = AllergenStatusBool.No;
        public AllergenStatusBool AllergenNut { get; set; } = AllergenStatusBool.No;
        public AllergenStatusBool AllergenDairy { get; set; } = AllergenStatusBool.No;
        public AllergenStatusBool AllergenGluten { get; set; } = AllergenStatusBool.No;
        public AllergenStatusBool AllergenSpicy { get; set; } = AllergenStatusBool.No;
    }