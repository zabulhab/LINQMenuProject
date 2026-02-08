using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

    // Collection of DishIngredients
    public class DishIngredientList
    {
        [Key] // f
        public int Id { get; set; } 

        public int Dish_ID { get; private set; }

        public DishIngredientList( int id, int dish_ID )
        {
            //Id = System.Guid.NewGuid();
            Id = id;
            Dish_ID = dish_ID;
        }

    }