// Output: new List<Ingredient>(), new List<Dish>()
// also generates IDs as needed

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using EFTest;
using System.Linq;

public static class RecipeParser{
    
    public static (List<Dish>,List<Ingredient>/**, List<DishIngredientList>, List<DishIngredient>**/) GetDishesAndIngredients( string filepath, EFTest.MenuContext mc ){
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

        List<JsonRecipeMapping.JsonRecipeContext> parsedRecipes = JsonSerializer.Deserialize<List<JsonRecipeMapping.JsonRecipeContext>>(text, new JsonSerializerOptions{ PropertyNameCaseInsensitive = true });
        
        List<Dish> dishes = new List<Dish>();
        List<Ingredient> ingredients = new List<Ingredient>();
        List<DishIngredientList> dishIngredientLists = new List<DishIngredientList>();
        List<DishIngredient> dishIngredients = new List<DishIngredient>();

        foreach ( JsonRecipeMapping.JsonRecipeContext recipe in parsedRecipes )
        {
            Dish dish = new Dish();
            dish.Id = recipe._Id;
            dish.Name = recipe.RecipeName;

            foreach (JsonRecipeMapping.JsonIngredientContext ingredientJson in recipe.Ingredients)
            {
                //int ingredient_ID = 
                Ingredient ingredient = new Ingredient() { Name = ingredientJson.Name };
                ingredient.Id = ingredientJson.Id; 
                //DishIngredient dishIngredient = new DishIngredient() { Ingredient_ID =  };
                             
                try
                {
                    SetIngredientAllergensFromParsedJSON(mc, ingredientJson, ingredient);                    
                }
                catch
                {
                    break; // Go to next recipe if allergen info could not be set up
                }

                // Ensure no ingredients are duplicated
                List<Ingredient> existingIngredient = ingredients.Where( i => i.Name == ingredient.Name ).ToList();
                if ( existingIngredient.Count == 0 )
                {
                    ingredients.Add(ingredient);
                }

            }
            dishes.Add(dish);
        }

        return (dishes, ingredients);
    }

    private static void SetIngredientAllergensFromParsedJSON(MenuContext mc, JsonRecipeMapping.JsonIngredientContext ingredientJson, Ingredient newIngredient)
    {
        foreach (JsonRecipeMapping.JsonAllergenRepresentation allergen in ingredientJson.Details.Allergens)
        {
            // Check if allergen is defined in project context
            if (mc.Allergens.Contains(allergen.AllergenName))
            {
                AllergenStatusBool status;
                if (allergen.AllergenStatus == "Yes")
                {
                    status = AllergenStatusBool.Yes;
                }
                else if (allergen.AllergenStatus == "No")
                {
                    status = AllergenStatusBool.No;
                }
                else if (allergen.AllergenStatus == "Maybe")
                {
                    status = AllergenStatusBool.Maybe;
                }
                else
                {
                    Console.Error.WriteLine("Error: " + allergen.AllergenStatus +
                                      " is not a valid allergen status for Ingredient " +
                                      ingredientJson.Name + ". Please provide a value of Yes, No, or Maybe");
                    throw new Exception();
                }

                // Assign allergen status to relevant allergen
                // TODO: refactor to use dictionary?
                if (allergen.AllergenName == "Dairy")
                {
                    newIngredient.AllergenDairy = status;
                }
                else if (allergen.AllergenName == "Gluten")
                {
                    newIngredient.AllergenGluten = status;
                }
                else if (allergen.AllergenName == "Meat")
                {
                    newIngredient.AllergenMeat = status;
                }
                else if (allergen.AllergenName == "Nut")
                {
                    newIngredient.AllergenNut = status;
                }
                else if (allergen.AllergenName == "Sesame")
                {
                    newIngredient.AllergenSesame = status;
                }
                else if (allergen.AllergenName == "Spicy")
                {
                    newIngredient.AllergenSpicy = status;
                }
            }
            else
            {
                Console.Error.WriteLine("Error: " + allergen.AllergenName +
                                      " is an undefined allergen." );
                throw new Exception();
            }
        }
    }
}