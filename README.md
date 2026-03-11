A .NET Core 10.0 REST Web API written in C#. The data represents restaurant menu items "Dishes" and their ingredients and other info about them, and is stored in Menu.db and accessed through SQLite and Entity Framework. There are controllers for Dishes and Ingredients, which call their respective services to handle business logic and accessing the DbContext. In the MenuAPI.http file there is a collection of requests that allow testing each of the currently implemented controller handler methods. There is also a method in Program.db that reads in a JSON file (such as the provided sampleinput.json) and uses it to fill in the Recipes.db database, accesible through an additional runtime argument.

See the Wiki tab for progress update and current status. 

Database Architecture:
Database is divided into tables Dishes, DishIngredientList, DishIngredients, and Ingredients
Dishes store their name and ID
DishIngredientList are 1:1 to Dishes and have a foreign key referencing the Dish ID.
DishIngredient entries have a foreign key referencing an Ingredient and a foreign key referencing a DishIngredientList. A dishIngredient represents the link between a DishIngredientList and an Ingredient, and in this way, they allow removal of Ingredients from Dishes without deleting the actual Ingredient from the database.
Ingredients store only their ID, name, and allergen info. If removed, the Dishes referencing them are also deleted (this behavior may be altered later).

Usage: 
run API normally: 
dotnet watch, dotnet run watch
read in input from a json file to fill in the sqlite database before running the API: 
dotnet watch --[json file name, e.g. --sampleinput.json] 
dotnet run watch --[json file name, e.g. --sampleinput.json] 

Use the MenuAPI.http file or the generated Swagger web page (e.g. https://localhost:7085/swagger/index.html) to test the different available API requests.
