using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DrinksMenu
{
    internal record class Categories(
        [property: JsonPropertyName("drinks")] List<Category> Drinks);

    internal record class Category(
        [property: JsonPropertyName("strCategory")] string Name);
}
