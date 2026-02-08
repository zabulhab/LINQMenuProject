[Work in Progress] 
My first basic project using the C# Entity Framework and LINQ. 
For now, stores dishes and their ingredients in a MySQL database then finds all dishes containing the "gluten" allergen. These are printed to the console. 
The next step is changing from hard-coded "recipes" to reading in JSON files defining what ingredients are in a dish and what allergens are in an ingredient.
After that I plan to make a front-end allowing users to filter existing recipes and submit their own new dishes and ingredients.

Divided into 4 tables: Dishes, DishIngredientList, DishIngredients, Ingredients
Dishes have a foreign key referencing their DishIngredientList_ID and DishIngredientLists have a foreign key referencing their Dish_ID.
DishIngredients have a foreign key referencing an Ingredient and a foreign key referencing a DishIngredientList.
Ingredients store only their ID, name, and allergen info.
