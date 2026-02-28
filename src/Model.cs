using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EFTest
{
    public class MenuContext : DbContext
    {
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<DishIngredientList> DishIngredientList { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public readonly List<string> Allergens = new List<string>()
            {"Dairy", "Gluten", "Meat", "Nut", "Sesame", "Spicy"};

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=Recipes.db");
    }

}