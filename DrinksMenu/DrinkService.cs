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
    internal class DrinkService
    {
        private string BaseUrl { get; set; }
        private string ListCategoriesUrlFragment { get; set; }
        private string ListDrinksByCategoryQueryUrlFragment { get; set; }
        private string DrinkQueryUrlFragment { get; set; }
        private RestClient Client { get; set; }


        public DrinkService()
        {
            BaseUrl = ConfigurationManager.AppSettings.Get("baseUrl");
            ListCategoriesUrlFragment = ConfigurationManager.AppSettings.Get("listDrinkCategories");
            ListDrinksByCategoryQueryUrlFragment = ConfigurationManager.AppSettings.Get("listDrinksByCategoryQuery");
            DrinkQueryUrlFragment = "www.thecocktaildb.com/api/json/v1/1/lookup.php?i=";

            Client = new RestClient(BaseUrl);


        }

        public List<Category> GetCategories()
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




        public List<Drink> GetDrinksByCategory(string category)
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

        public DrinkDetail GetDrinkById(string id)
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
