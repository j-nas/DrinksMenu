using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrinksMenu.Models;
using Spectre.Console;

namespace DrinksMenu
{
    internal class DrinkMenu
    {
        List<Drink> Drinks { get; set; }

        public DrinkMenu(List<Drink> drinks)
        {
            Drinks = drinks;
        }

        public string Render()
        {
            AnsiConsole.Write(
                new FigletText("Drink Menu")
                    .Centered()
                    .Color(Color.Magenta1)
                );
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]Select drink[/]")
                    .PageSize(10)
                    .AddChoices(Drinks.Select(x => x.strDrink).ToList()));

            return Drinks.Where(x => x.strDrink == selection).FirstOrDefault().idDrink;
        }
    }
}
