// Output: new List<Ingredient>(), new List<Dish>()
// also generates IDs as needed

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class RecipeParser{
    
    public static List<Object> get_dishes_and_ingredients( string filepath ){
        // read file json into continous string
        string text = "";

        try{
            text = File.ReadAllText(filepath);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Error: The file '{filepath}' was not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        // TODO: Make this work
        List<JsonRecipeMapping.JsonRecipeContext> parsedRecipes = JsonSerializer.Deserialize<List<JsonRecipeMapping.JsonRecipeContext>>(text, new JsonSerializerOptions{ PropertyNameCaseInsensitive = true });
        
        List<Dish> dishes = new List<Dish>();
        List<Ingredient> ingredients = new List<Ingredient>();

        foreach ( JsonRecipeMapping.JsonRecipeContext recipe in parsedRecipes )
        {
            dishes.Add(new Dish() { Name = recipe.RecipeName, Id = recipe._Id });
            foreach (JsonRecipeMapping.JsonIngredientContext ingredient in recipe.Ingredients)
            {
                // TODO: Update to use allergybool and check by looping through a list of all defined allergens (provide method)
                Ingredient newIngredient = new Ingredient(){ Name = ingredient.Name };
                newIngredient.Id = ingredient.Id;newIngredient.AllergenDairy = ingredient.Details.Allergens.Contains("dairy")? AllergenStatusBool.Yes : AllergenStatusBool.No;
                
                if (!ingredients.Contains(newIngredient))
                {
                    ingredients.Add(newIngredient);
                }
                
            }
        }

        return new List<Object>(){dishes, ingredients};
    }
    
}