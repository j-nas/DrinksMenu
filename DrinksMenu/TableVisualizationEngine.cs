using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using DrinksMenu.Models;

namespace DrinksMenu
{
    internal static class TableVisualizationEngine
    {
        public static void RenderTable(DrinkDetail data)
        {
            
            var infoTable = new Table();

            infoTable.Title($"[green]{data.strDrink}[/]");
            infoTable.AddColumn(new TableColumn("Drink Category").Centered());
            infoTable.AddColumn(new TableColumn($"{data.strCategory}").Centered());
            infoTable.AddRow("Glass", data.strGlass);
            infoTable.Expand();

            var instructionsTable = new Table();
            
            instructionsTable.Title($"{data.strInstructions}");
            instructionsTable.AddColumn(new TableColumn("Ingredients").Centered());
            instructionsTable.AddColumn(new TableColumn("Measurements").Centered());
            instructionsTable.Expand();

            for (int i = 1; i < 16; i++)
            {
                string ingredient = $"strIngredient{i}";
                string measurement = $"strMeasure{i}";

                if (data.GetType().GetProperty(ingredient).GetValue(data, null) != null)
                {
                    string ingredientValue = data.GetType().GetProperty(ingredient).GetValue(data, null).ToString();
                    string measurementValue = data.GetType().GetProperty(measurement).GetValue(data, null).ToString();


                    instructionsTable.AddRow(ingredientValue, measurementValue);
                }
            }
            
            var layout = new Panel(new Rows(
                infoTable,
                instructionsTable
            ));
            layout.Expand();

            AnsiConsole.Write(layout);
            
            AnsiConsole.WriteLine("Press enter to return to the main menu");
            Console.ReadLine();
        }
        
    }
}
