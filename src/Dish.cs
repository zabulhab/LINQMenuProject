using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

    // Composed of DishIngredients
    public class Dish
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        // Ingredients in this recipe, using HashSet to prevent duplicates
        //public HashSet<DishIngredient> DishIngredients { get; set; } = new HashSet<DishIngredient>();
        //public DishIngredient DishIngredients { get; set; }
        // public DishIngredientList DishIngredientList 
        // { 
        //     get{ return DishIngredientList; } 
        //     set
        //     { 
        //         DishIngredientList = value;
        //         DishIngredientList_ID = value.Id;
        //     } 
        // }
        public int DishIngredientList_ID { get; set; }

        public Dish( int id, string name = null )
        {
            Id = id;
            Name = name;
        }

    }