using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using DrinksMenu.Models;

namespace DrinksMenu
{
    internal class TableVisualizationEngine
    {
        private DrinkDetail Data { get; set; }
        private Table InfoTable { get; set; }
        private Table InstructionsTable { get; set; }
        public TableVisualizationEngine(DrinkDetail data)
        {
            Data = data;
            InfoTable = new Table();

            InfoTable.Title($"[green]{Data.strDrink}[/]");

            InfoTable.AddColumn(new TableColumn("Drink Name").Centered());
            InfoTable.AddColumn(new TableColumn($"[green]{Data.strDrink}[/]").Centered());
            //InfoTable.AddRow("Category", Data.strCategory);
            //InfoTable.AddRow("Alcoholic", Data.strAlcoholic);
            //InfoTable.AddRow("Glass", Data.strGlass);

            InstructionsTable = new Table();
            InstructionsTable.Title($"[green]{Data.strInstructions}[/]");
            InstructionsTable.AddColumn(new TableColumn("Ingredients").Centered());
            InstructionsTable.AddColumn(new TableColumn("Measurements").Centered());

            for (int i = 1; i < 16; i++)
            {
                string ingredient = $"strIngredient{i}";
                string measurement = $"strMeasure{i}";

                if (Data.GetType().GetProperty(ingredient).GetValue(Data, null) != null)
                {
                    string ingredientValue = Data.GetType().GetProperty(ingredient).GetValue(Data, null).ToString();
                    string measurementValue = Data.GetType().GetProperty(measurement).GetValue(Data, null).ToString();


                    InstructionsTable.AddRow(ingredientValue, measurementValue);
                }
            }
        }
        public void Render()
        {
            AnsiConsole.Write(new Columns(
                new Panel(InfoTable).Expand(),
                new Panel(InstructionsTable).Expand()
            ));
        }
    }
}
