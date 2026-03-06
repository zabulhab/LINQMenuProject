using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuAPI.Models;

    // Collection of DishIngredients
    public class DishIngredientList
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)] // f
        public long Id { get; private set; } 

        public long Dish_ID { get; private set; }

        //public string Dish_Name { get; set; }

        //public Dish Dish { get; set; }

        public DishIngredientList(long dish_ID )
        {
            Id = MenuAPI.MenuContext.IdGenerator.CreateId();
            Dish_ID = dish_ID;
        }

    }
