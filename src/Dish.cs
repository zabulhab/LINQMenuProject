using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

// Composed of Ingredients obtainable through DishIngredientList
public class Dish
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; private set; }
        public string Name { get; set; }

        // removed to avoid circular dependency
        //public long DishIngredientList_ID { get; set; }

        public Dish( string name = null )
        {
            Id = EFTest.MenuContext.IdGenerator.CreateId();
            if (name != null) Name = name;
        }
    }