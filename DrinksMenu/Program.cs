using DrinksMenu;
using Spectre.Console;
using System.Net.Http.Headers;
using System.Configuration;
using System.Collections.Specialized;
using System.Text.Json.Nodes;
using System.Text.Json;

var baseUrl = ConfigurationManager.AppSettings.Get("baseUrl");
var listDrinkCategories = ConfigurationManager.AppSettings.Get("listDrinkCategories");
var listDrinksByCategoryQuery = ConfigurationManager.AppSettings.Get("listDrinksByCategoryQuery");

List<string> categoryList = new();

using HttpClient client = new();
client.DefaultRequestHeaders.Clear();
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
client.DefaultRequestHeaders.Add("User-Agent", "DrinkMenu");
await ProcessDrinkCategoriesAsync(client);

async Task ProcessDrinkCategoriesAsync(HttpClient client)
{
    await using Stream stream = await client.GetStreamAsync($"{baseUrl}{listDrinkCategories}");
    var categories = await JsonSerializer.DeserializeAsync<Categories>(stream);

    foreach (var category in categories.Drinks)
    {
        categoryList.Add(category.Name);
    }
}

AnsiConsole.Write(new FigletText("Drink Menu").Centered().Color(Color.Magenta3_2));
var selection = AnsiConsole.Prompt(
    new SelectionPrompt<string>().Title("[green]Select a drink category[/]").AddChoices(categoryList));

await ProcessDrinksAsync(client, selection);

List<string> drinkList = new();

async Task ProcessDrinksAsync(HttpClient client, string selection)
{
    await using Stream stream = await client.GetStreamAsync($"{baseUrl}{listDrinksByCategoryQuery}{selection}");
    var drinks = await JsonSerializer.DeserializeAsync<DrinkList>(stream);
    foreach (var drink in drinks.Drinks)
    {
        Console.WriteLine(drink.Name);
    }
}
Console.ReadLine();
//Console.Clear();

//AnsiConsole.Write(new FigletText("Drink Menu").Centered().Color(Color.Magenta3_2));
//var drinkSelection = AnsiConsole.Prompt(
//    new SelectionPrompt<string>().Title("[green]Select a drink[/]").AddChoices(drinkList));
