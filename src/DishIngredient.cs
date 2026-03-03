using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

    // Represents an Ingredient inside of a DishIngredientList
    public class DishIngredient 
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; private set; }
        public long DishIngredientList_ID { get; private set; }
        public long Ingredient_ID { get; private set; }

        public DishIngredient( long dishIngredientList_ID, long ingredient_ID ) 
        {
            Id = EFTest.MenuContext.IdGenerator.CreateId();
            DishIngredientList_ID = dishIngredientList_ID;
            Ingredient_ID = ingredient_ID;
         }
    }