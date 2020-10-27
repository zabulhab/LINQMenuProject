using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EFTest
{
    public class MenuContext : DbContext
    {
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=Recipes.db");
        //public MenuContext(DbContextOptions<MenuContext> options): base(options)
        //{
        //    Database.EnsureCreated();
        //}
    }

    // Food composed of ingredients
    public class Dish
    {
        public int ID { get; set; }
        public string Name { get; set; }

        // Ingredients in this recipe
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }

    public class Ingredient
    {
        [Key] // f
        public string Name { get; set; } // unique identifier

        public bool IsMeat { get; set; } = false;
        public bool IsNut { get; set; } = false;
        public bool HasDairy { get; set; } = false;
        public bool HasGluten { get; set; } = false;
        public bool IsSpicy { get; set; } = false;
    }
}