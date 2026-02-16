using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

// TODO: Make JSON storing recipes to tell dishes which ingredients they contain?
// TODO: Make JSON storing ingredients and their allergen info
// TODO: turn IDs into static final constants in another class
// TODO: Add OnDelete to foreign keys in table defs
// My first time using the Entity Framework with LINQ queries
namespace EFTest
{

    class Program
    {
        //TODO: pass in file name input to read 
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Error: Please provide input recipe file");
            }
            
            string filepath = args[0];
            using var db = new MenuContext();

            List<object> parsedRecipes = RecipeParser.get_dishes_and_ingredients(filepath);

            // TODO: get input from json parser instead

            List<Ingredient> newIngredients = new List<Ingredient>();
            newIngredients.Add(new Ingredient { Id = 1, Name = "Apple" });
            newIngredients.Add(new Ingredient { Id = 2, Name = "Sugar" });
            newIngredients.Add(new Ingredient { Id = 3, Name = "Flour", AllergenGluten = AllergenStatusBool.Yes });
            newIngredients.Add(new Ingredient { Id = 4, Name = "Butter", AllergenDairy = AllergenStatusBool.Yes });
            newIngredients.Add(new Ingredient { Id = 5, Name = "Beef", AllergenMeat = AllergenStatusBool.Yes });

            List<Dish> newDishes = new List<Dish>();
            newDishes.Add( new Dish( 1, "Salad") );
            newDishes.Add( new Dish( 2, "Apple Pie") );
            newDishes.Add(new Dish( 3, "Meat Pie" ));

            foreach (Dish dish in newDishes)
            {
                if ( !db.Dishes.Any( d => d.Id ==  dish.Id) ) {
                    Console.WriteLine("Inserting new dish " + dish.Name + " into dishes table");
                    db.Dishes.Add(dish);
                 }
            }

            foreach (Ingredient ingredient in newIngredients)
            {
                if ( !db.Ingredients.Any( i => i.Id == ingredient.Id ) ) {
                    Console.WriteLine("Inserting new ingredient " + ingredient.Name + " into ingredients table");
                    db.Ingredients.Add(ingredient);
                 }
            }

            db.SaveChanges();

            // Lists to store dish ingredient lists and dish ingredients
            List<DishIngredientList> newDishIngredientLists = new List<DishIngredientList>();
            List<List<DishIngredient>> newDishIngredients = new List<List<DishIngredient>>();

            // create and add new dish ingredient lists and dish ingredients to lists
            newDishIngredientLists.Add( new DishIngredientList( ConstantNumbers.APPLE_PIE_INGREDIENTLIST_ID, 
                                                                                ConstantNumbers.APPLE_PIE_DISH_ID ) );
            newDishIngredientLists.Add( new DishIngredientList( ConstantNumbers.MEAT_PIE_INGREDIENTLIST_ID, 
                                                                                ConstantNumbers.MEAT_PIE_DISH_ID ) );
            
            newDishIngredients.Add( getDishIngredients( db, 
                                                        ConstantNumbers.APPLE_PIE_INGREDIENTLIST_ID, 
                                                        new String[]{"Apple", "Sugar", "Flour", "Butter"}));
            newDishIngredients.Add( getDishIngredients( db, 
                                                        ConstantNumbers.MEAT_PIE_INGREDIENTLIST_ID, 
                                                        new String[]{"Beef", "Sugar", "Flour", "Butter"}));

            // For each new dish, add its DishIngredientList and its DishIngredients to db

            foreach (DishIngredientList dil in newDishIngredientLists)
            {
                if ( !db.DishIngredientList.Any( i => i.Id == dil.Id ) ) 
                {
                    Console.WriteLine("Storing the ingredients for " + db.Dishes.First(d => d.Id == dil.Dish_ID).Name);
                    db.DishIngredientList.Add(dil);
                }
            }
            foreach (List<DishIngredient> di in newDishIngredients)
            {
                // TODO: use update and delete to change ingredients stored in dish ingredients
                foreach ( DishIngredient item in di ) // add any dish ingredient not already saved
                {
                    if ( !db.DishIngredients.Any( i => i.Id == item.Id && 
                                                  i.DishIngredientList_ID == item.DishIngredientList_ID ) ) {
                        db.DishIngredients.Add(item);
                    }
                }
            }

            // Update DishIngredientList_ID references in dishes

            var applePie = db.Dishes.First(d => d.Id == ConstantNumbers.APPLE_PIE_DISH_ID);
            applePie.DishIngredientList_ID = ConstantNumbers.APPLE_PIE_INGREDIENTLIST_ID;

            var meatPie = db.Dishes.First(d => d.Id == ConstantNumbers.MEAT_PIE_DISH_ID);
            meatPie.DishIngredientList_ID = ConstantNumbers.MEAT_PIE_INGREDIENTLIST_ID;
            
            db.SaveChanges();

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

            //from dishes in db.Dishes where dishes.Ingredients.

            // Delete
            Console.WriteLine("Deleting salad dish from menu");
            db.Remove(db.Dishes.First(d => d.Id == 01));

            db.SaveChanges();
        }

    private static List<DishIngredient> getDishIngredients(MenuContext db, int dishIngredientListID, string[] ingredients)
        {
            List<DishIngredient> dishComposition = new List<DishIngredient>();
            for (int i = 0; i < ingredients.Length; i++)
            {
                dishComposition.Add( new DishIngredient( 
                                        dishIngredientListID, 
                                        int.Parse(dishIngredientListID.ToString() + (i+1).ToString()), 
                                        db.Ingredients.First(ing => ing.Name == ingredients[i]).Id ) );
            }
            return dishComposition;
        }
    }

    public static class ConstantNumbers
    {
        public static readonly int SALAD_PIE_DISH_ID = 1;
        public static readonly int APPLE_PIE_DISH_ID = 2;
        public static readonly int MEAT_PIE_DISH_ID = 3;

        public static readonly int APPLE_PIE_INGREDIENTLIST_ID = 1;
        public static readonly int MEAT_PIE_INGREDIENTLIST_ID = 2;
    }
}