using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrinksMenu.Models;

namespace DrinksMenu
{
    internal class CategoryMenu
    {
        List<Category> Categories { get; set; }

        public CategoryMenu(List<Category> categories)
        {
            Categories = categories;
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
                    .Title("[green]Select from a base liquor[/]")
                    .PageSize(10)
                    .AddChoices(Categories.Select(x => x.strCategory).ToList()));

            return selection;
        }
       
    }
}
