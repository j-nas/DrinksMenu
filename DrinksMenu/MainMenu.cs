using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksMenu
{
    internal class MainMenu
    {
        public static void RenderMainMenu()
        {
            AnsiConsole.Write(
                new FigletText("Drink Menu")
                    .Centered()
                    .Color(Color.Magenta1)
                );
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]Select from a base liquor[/]")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "Vodka", "Gin", "Tequila", "Rye", "Rum", "Brandy", "Other"
                    }));
            MenuSelection(selection);


            switch (selection)
            {
                case "Vodka":
                    AnsiConsole.WriteLine("You have selected vodka");
                    break;
                default:
                    break;
            }
        }
        public static Action MenuSelection(string selection) => selection switch
        {
            "Vodka" => () => { AnsiConsole.WriteLine("Vodka"); },
            _ => () => Console.WriteLine("help")
        };
        
    }
}
