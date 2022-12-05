using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DrinksMenu
{
    internal class DrinksClient
    {
        
        public static async Task GetDrinks()
        {
            using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "DrinkMenu");

            await ProcessDrinksAsync(client);

            
        }

        public static async Task ProcessDrinksAsync(HttpClient client)
        {
            await using Stream stream = await client.GetStreamAsync("https://www.thecocktaildb.com/api/json/v1/1/filter.php?c=Cocktail");
            var drinks = await JsonSerializer.DeserializeAsync<DrinkList>(stream);

            Console.WriteLine(drinks.Drinks);
            foreach (var drink in drinks.Drinks)
            {
                Console.WriteLine(drink.Name);
            }
        }
    }
}
