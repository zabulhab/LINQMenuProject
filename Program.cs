using System;
using System.Linq;
using System.Collections.Generic;

// My first time using the Entity Framework with LINQ queries
namespace EFTest
{
    // Makes a new person context, into which you can add people to the people
    // table and meals to another table
    class Program
    {
        static void Main()
        {
            using var db = new MenuContext();
            Console.WriteLine("Inserting a salad into dishes table");
            db.Add(new Dish { Name = "Salad", ID = 01 });

            Console.WriteLine("Inserting 5 different ingredients into ingredient table");
            db.Add(new Ingredient { Name = "Apple" });
            db.Add(new Ingredient { Name = "Sugar" });
            db.Add(new Ingredient { Name = "Flour", HasGluten = true });
            db.Add(new Ingredient { Name = "Butter", HasDairy = true });
            db.Add(new Ingredient { Name = "Beef", IsMeat = true });


            Console.WriteLine("Inserting apple and meat pies into dishes table");
            db.Add(new Dish { Name = "Apple Pie", ID = 02 });
            db.Add(new Dish { Name = "Meat Pie", ID = 03 });

            db.SaveChanges();

            var applePie = db.Dishes.First(d => d.ID == 02);
            // store apple pie ingredients by retrieving them from ingredients db
            Console.WriteLine("Storing the ingredients for dish");
            applePie.Ingredients.Add(
                db.Ingredients.First(i => i.Name == "Apple")
                );
            applePie.Ingredients.Add(
                db.Ingredients.First(i => i.Name == "Sugar")
                );
            applePie.Ingredients.Add(
                db.Ingredients.First(i => i.Name == "Flour")
            );
            applePie.Ingredients.Add(
                db.Ingredients.First(i => i.Name == "Butter")
            );
            db.SaveChanges();

            // make meat pie by slightly adjusting apple pie ingredients
            var meatPie = db.Dishes.First(d => d.ID == 03);
            foreach (Ingredient i in applePie.Ingredients)
            {
                meatPie.Ingredients.Add(i);
            };
            meatPie.Ingredients.Remove(db.Ingredients.First(i => i.Name == "Apple"));
            meatPie.Ingredients.Add(db.Ingredients.First(i => i.Name == "Beef"));
            meatPie.Name = "Grandma's Meat Pie"; // change name of meat pie

            db.SaveChanges();

            Console.WriteLine("Menu:");
            foreach (Dish dish in db.Dishes)
            {
                Console.WriteLine(dish.Name);
            }

            // Print meatless food
            var meat = db.Ingredients.Where(i => i.IsMeat);
            var withoutMeat = db.Dishes.Where(d => !meat.Any(i => d.Ingredients.Contains(i)));
            //var withoutMeat = db.Dishes.Where(i => !i.Ingredients.Contains(j=>j.IsMeat));
            //var rejectList = db.Dishes.Where(d => !meat.Contains(d.Ingredients));
            //var filteredList = fullList.Except(rejectList);
            Console.WriteLine("Vegetarian Dishes:");
            foreach (var dish in withoutMeat)
            {
                Console.WriteLine(dish.Name); 
            }

            //from dishes in db.Dishes where dishes.Ingredients.

            // Delete
            Console.WriteLine("Deleting salad dish from menu");
            db.Remove(db.Dishes.First(d => d.ID == 01));

            db.SaveChanges();
        }
    }
}