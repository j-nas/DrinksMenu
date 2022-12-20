using DrinksMenu;
using Spectre.Console;
using System.Net.Http.Headers;
using System.Configuration;
using System.Collections.Specialized;
using System.Text.Json.Nodes;
using System.Text.Json;

var drinkService = new DrinkService();

var categories = drinkService.GetCategories();

var categoryMenu = new CategoryMenu(categories);

var selectedCategory = categoryMenu.Render();

var drinks = drinkService.GetDrinksByCategory(selectedCategory);

var drinkMenu = new DrinkMenu(drinks);

var selectedDrink = drinkMenu.Render();

var drink = drinkService.GetDrinkById(selectedDrink);

var drinkDetail = new TableVisualizationEngine(drink);

drinkDetail.Render();