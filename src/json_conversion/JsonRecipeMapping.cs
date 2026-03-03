// Mapping class to deserialize JSON to intermediate objects that 
// represent the JSON's structure for later use in building db sets
using System.Collections.Generic;

namespace EFTest
{
    public class JsonRecipeMapping
    {
        public JsonRecipeMapping()
        {
        }
        
        public class JsonRecipeContext
        {
            public int Id { get; set; }
            public int Index { get; set; }
            public string RecipeName { get; set; }
            public List<JsonIngredientContext> Ingredients { get; set; }
        }
        
        public class JsonIngredientContext
        {
            public string Name { get; set; }
            public int Id { get; set; }
            public JsonIngredientDetails Details { get; set; }
        }

        public class JsonIngredientDetails
        {
            public List<JsonAllergenRepresentation> Allergens { get; set; }
            public int Portion { get; set; }
            public string PortionUnit { get; set; }
        }

        public class JsonAllergenRepresentation
        {
            public string AllergenName { get; set; }
            public string AllergenStatus { get; set; } 
        }
    }
}
