using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

// TODO: use update and delete to change ingredients stored in dish ingredients
// TODO: Add OnDelete to foreign keys in table defs
// My first time using the Entity Framework with LINQ queries
namespace EFTest
{

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Error: Please provide input recipe file");
                return;
            }
            
            string filepath = args[0];
            using var db = new MenuContext();

            (List<Dish> ParsedDishes,
            List<Ingredient> ParsedIngredients,
            List<DishIngredientList> ParsedDishIngredientLists,
            List<DishIngredient> ParsedDishIngredients) 
                = RecipeParser.GetDishesAndIngredients(filepath, db);

            if ( ParsedDishes.Count < 1 || ParsedIngredients.Count < 1 )
            {
                Console.WriteLine("Error: No data could be derived from JSON input");
                return;
            }

            foreach (Ingredient ingredient in ParsedIngredients)
            {
                if ( !db.Ingredients.Any( i => i.Name == ingredient.Name ) ) {
                    Console.WriteLine("Inserting new ingredient " + ingredient.Name + " into ingredients table");
                    db.Ingredients.Add(ingredient);
                 }
            }
            db.SaveChanges();

            foreach (Dish dish in ParsedDishes)
            {
                if ( !db.Dishes.Any( d => d.Name ==  dish.Name) ) {
                    Console.WriteLine("Inserting new dish " + dish.Name + " into dishes table");
                    db.Dishes.Add(dish);
                 }
            }
            db.SaveChanges();

            foreach (DishIngredientList dil in ParsedDishIngredientLists)
            {
                string dishName = "";
                try 
                { 
                    dishName = ParsedDishes.First(d => d.Id == dil.Dish_ID).Name; 
                }
                catch
                {
                    Console.WriteLine($"No provided dish with name {dil.Dish_ID}");
                }
                // If a DishIngredientList doesn't yet exist for this Dish
                if ( !db.DishIngredientList.Any( i => i.Dish_Name == dishName ) ) 
                {
                    Console.WriteLine("Storing the ingredients for " + dishName );
                    db.DishIngredientList.Add(dil);
                }
                else
                {
                    Console.WriteLine("Error: Tried to overwrite a Dish's exisitng DishIngredientList through Add");
                }
            }
            db.SaveChanges();
            
            // TODO: change to use UPDATE on existing DishIngredientList/s if trying ADD with newDishIngredients
            foreach (DishIngredient di in ParsedDishIngredients)
            {
                string dishName = "";
                DishIngredientList savedDishIngredientList;
                try 
                { 
                    savedDishIngredientList = db.DishIngredientList.First(d => d.Id == di.DishIngredientList_ID); 
                }
                catch
                {
                    // 
                }
                // If DishIngredientList with same DishName exists, change di.DishIngredientList_ID before adding 
                // DishIngredients to DB, if those ingredients don't already exist on the DishIngredient table
                // 
                if ( !db.DishIngredientList.Any(dil=>dil.Id == di.DishIngredientList_ID) )
                {
                    Console.WriteLine("Error: Tried to add DishIngredients to a non-existent DishIngredientList through Add");
                }   
                else if (db.DishIngredientList.Contains(dil=>dil.Name==di))
                if ( !db.DishIngredients.Any( i => i.Id == di.Id && 
                                                i.DishIngredientList_ID == di.DishIngredientList_ID ) ) {
                    db.DishIngredients.Add(di);
                }
            }
            db.SaveChanges();


            // Now that all tables and the tables they depend on have been set up, save them
            //db.SaveChanges();


            Console.WriteLine("Menu:");
            foreach (Dish dish in db.Dishes)
            {
                Console.WriteLine(dish.Name);
            }

            // Get food with gluten
            var glutenIngredients = db.Ingredients.Where(i => i.AllergenGluten == AllergenStatusBool.Yes);
            var glutenDishIngredients = db.DishIngredients.Where( di => glutenIngredients.Any( i => i.Id == di.Ingredient_ID ) == true );
            var glutenDishIngredientLists = db.DishIngredientList.Where( dil => glutenDishIngredients.Any( i=> i.DishIngredientList_ID == dil.Id ) );
            var glutenDishes = db.Dishes.Where( d=> glutenDishIngredientLists.Any( i=> i.Dish_ID == d.Id ) );
            
            Console.WriteLine("Gluten-containing Dishes:");
            foreach (var dish in glutenDishes)
            {
                Console.WriteLine(dish.Name); 
            }
            
            // Remove salad if it exists
            try     
            {    
                db.Remove(db.Dishes.First(d => d.Id == db.Dishes.First(i =>i.Name == "salad").Id));
                Console.WriteLine("Deleting salad dish from menu");
            }
            catch 
            {
                Console.WriteLine("No salad dish on menu, could not delete");
                return;
            }

            db.SaveChanges();
        }
    }
}