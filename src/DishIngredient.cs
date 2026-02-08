using System.ComponentModel.DataAnnotations;

    // Represents an Ingredient inside of a DishIngredientList
    public class DishIngredient 
    {
        [Key]
        public int Id { get; private set; }
        public int DishIngredientList_ID { get; private set; }
        public int Ingredient_ID { get; private set; }

        public DishIngredient( int dishIngredientList_ID, int id, int ingredient_ID ) {
            DishIngredientList_ID = dishIngredientList_ID;
            Id = id;
            //Id = System.Guid.NewGuid();
            Ingredient_ID = ingredient_ID;
         }
    }