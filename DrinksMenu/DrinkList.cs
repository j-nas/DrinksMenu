using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DrinksMenu
{
    public record class DrinkList(
        [property: JsonPropertyName("drinks")]  List<Drink> Drinks);

    public record class Drink(
        [property: JsonPropertyName("strDrink")] string Name);
}
