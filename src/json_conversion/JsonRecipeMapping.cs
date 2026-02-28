using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic;

//public class RecipesJSONSourceFormat
// {
// 
//   [JsonPropertyName("_id")]
//   public string Id { get; set; }
//   [JsonPropertyName("index")]
//   public int Index { get; set; }
//   [JsonPropertyName("recipeName")]
//   public string RecipeName { get; set; }
//   [JsonPropertyName("ingredients")]
//   public List<KeyValuePair<string, List<List<string>>>> Ingredients { get; set; }
// }

public class JsonRecipeMapping
{
    public JsonRecipeMapping(/*RecipesJSONSourceFormat source*/)
    {
    //    Id = source.Id;
    //    Recipes = 
       //Dishes
    //   Index = new PVE { Kills = source.PVE };
    //   RecipeName
    //   Ingredients
    }
    
    // public string Id;

    // private List<JsonRecipeContext> Recipes { get; set; }
    // private List<JsonIngredientContext> Ingredients { get; set; }

    public class JsonRecipeContext
    {
        public int _Id { get; set; }
        // public System.Guid Guid {
        //     get { return Guid; }
        //     private set 
        //     { 
        //         if (Guid == null) 
        //         {
        //             Guid = System.Guid.NewGuid(); 
        //         }
        //     }
        // }
        public int Index { get; set; }
        public string RecipeName { get; set; }
        public List<JsonIngredientContext> Ingredients { get; set; }
    }
    // public string RecipeName { get; set; }
    // public List<KeyValuePair<string, List<string>>> Ingredients { get; set; }
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

