using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Configuration;
using RestSharp;
using Newtonsoft.Json;
using DrinksMenu.Models;

namespace DrinksMenu
{
    internal static class DrinkService
    {
        private static string BaseUrl { get; set; }
        private static string ListCategoriesUrlFragment { get; set; }
        private static string ListDrinksByCategoryQueryUrlFragment { get; set; }
        private static string DrinkQueryUrlFragment { get; set; }
        private static RestClient Client { get; set; }


        static DrinkService()
        {
            BaseUrl = ConfigurationManager.AppSettings.Get("baseUrl");
            ListCategoriesUrlFragment = ConfigurationManager.AppSettings.Get("listDrinkCategories");
            ListDrinksByCategoryQueryUrlFragment = ConfigurationManager.AppSettings.Get("listDrinksByCategoryQuery");
            DrinkQueryUrlFragment = "www.thecocktaildb.com/api/json/v1/1/lookup.php?i=";

            Client = new RestClient(BaseUrl);


        }

        public static List<Category> GetCategories()
        {
            var request = new RestRequest(ListCategoriesUrlFragment);
            var response = Client.ExecuteAsync(request);

            List<Category> categories = new();

            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                var serialize = JsonConvert.DeserializeObject<Categories>(rawResponse);

                categories = serialize.CategoriesList;
                return categories;
            }


            return categories;
        }




        public static List<Drink> GetDrinksByCategory(string category)
        {
            var request = new RestRequest(ListDrinksByCategoryQueryUrlFragment + category);
            var response = Client.ExecuteAsync(request);

            List<Drink> drinks = new();

            
            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                var serialize = JsonConvert.DeserializeObject<Drinks>(rawResponse);

                drinks = serialize.drinks;
                return drinks;
            }

            return drinks;
        }

        public static DrinkDetail GetDrinkById(string id)
        {
            var request = new RestRequest(DrinkQueryUrlFragment + id);
            var response = Client.ExecuteAsync(request);

            DrinkDetail drink = new();

            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                var serialize = JsonConvert.DeserializeObject<DrinkDetail>(rawResponse);

                drink = serialize;
                return drink;
            }

            return drink;
        }


    }
}
