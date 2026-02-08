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

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=Recipes.db");
    }

}