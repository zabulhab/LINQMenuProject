// Used to convert JSON input to Menu Context objects
// TODO: Define specific exception types
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using MenuAPI.Models;
using System.Linq;

namespace MenuAPI
{
    public static class RecipeParser
    {
        /// <summary>
        /// Uses input JSON file to create and return Menu Context DBset objects
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="mc">Menu Context Instance</param>
        /// <returns>Lists containing parsed dishes, ingredients, etc.</returns>
        public static 
            (List<Dish>, List<Ingredient>, List<DishIngredientList>, List<DishIngredient>)
            GetDishesAndIngredients( string filepath, MenuAPI.MenuContext mc )
        {
            // read file json into continous string
            string text = "";
            try
            {
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

            // Deserialize JSON to intermediate objects to prepare to create db sets
            List<JsonRecipeMapping.JsonRecipeContext> parsedRecipes = JsonSerializer.Deserialize<List<JsonRecipeMapping.JsonRecipeContext>>(text, new JsonSerializerOptions{ PropertyNameCaseInsensitive = true });
            
            List<Dish> dishes = new List<Dish>();
            List<Ingredient> ingredients = new List<Ingredient>();
            List<DishIngredientList> dishIngredientsLists = new List<DishIngredientList>();
            List<DishIngredient> dishIngredients = new List<DishIngredient>();

            foreach ( JsonRecipeMapping.JsonRecipeContext recipe in parsedRecipes )
            {
                Dish dish = new Dish()
                {
                    Name = recipe.RecipeName
                };
                DishIngredientList dishIngredientList = new DishIngredientList( dish.Id );
                //dish.DishIngredientList_ID = dishIngredientList.Id;

                foreach (JsonRecipeMapping.JsonIngredientContext ingredientJson in recipe.Ingredients)
                {
                    Ingredient ingredient;
                    if (!ingredients.Any(i=> i.Name == ingredientJson.Name ))
                    {
                        ingredient = new Ingredient( ingredientJson.Name );
                    }
                    else
                    {
                        ingredient = ingredients.First( i=> i.Name == ingredientJson.Name );
                    }

                    try
                    {
                        SetIngredientAllergensFromParsedJSON(mc, ingredientJson, ingredient);                    
                    }
                    catch
                    {
                        break; // Go to next recipe if allergen info could not be set up
                    }

                    DishIngredient dishIngredient = new DishIngredient( dishIngredientList.Id, ingredient.Id ); 
                    dishIngredients.Add(dishIngredient);

                    // Add ingredient to return list if not alredy existing
                    List<Ingredient> existingIngredient = ingredients.Where( i => i.Name == ingredient.Name ).ToList();
                    if ( existingIngredient.Count == 0 ) 
                    {
                        ingredients.Add(ingredient);
                    }

                }

                dishes.Add(dish);
                dishIngredientsLists.Add(dishIngredientList);
            }

            return (dishes, ingredients, dishIngredientsLists, dishIngredients);
        }

        private static void SetIngredientAllergensFromParsedJSON(MenuContext mc, JsonRecipeMapping.JsonIngredientContext ingredientJson, Ingredient newIngredient)
        {
            foreach (JsonRecipeMapping.JsonAllergenRepresentation allergen in ingredientJson.Details.Allergens)
            {
                // Check if allergen is defined in project context
                if (mc.Allergens.Contains(allergen.AllergenName))
                {
                    // Get allergen bool status 
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
                    // TODO: refactor to avoid abundant if statements?
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
                    Console.Error.WriteLine($"Error: '{allergen.AllergenName}' is an undefined allergen." );
                    throw new Exception();
                }
            }
        }
    }
}