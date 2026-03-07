[Work in Progress] 

My first basic project using the C# Entity Framework and ASP.NET Core. The data represents restaurant menu items "Dishes" and the ingredients and other info about them. What started as a console application that reads in a JSON file and uses it to fill in and query the Recipes.db database is now being refactored into an ASP.NET Core Web API. See the Wiki tab for current status. 

Divided into 4 tables: Dishes, DishIngredientList, DishIngredients, Ingredients
Dishes store their name and ID
DishIngredientLists have a foreign key referencing their Dish_ID.
DishIngredients have a foreign key referencing an Ingredient and a foreign key referencing a DishIngredientList.
Ingredients store only their ID, name, and allergen info.
