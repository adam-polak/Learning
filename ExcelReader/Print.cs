using ExcelReader.ExcelAccess.Models;
using Spectre.Console;

namespace ExcelReader;

public static class Print
{
    public static void PrintTable(List<Product> products)
    {
        Console.WriteLine("\n");
        Table table = new Table();
        table.Title("Products");
        table.AddColumn("Product");
        table.AddColumn("Units Sold");
        table.AddColumn("Profit");
        foreach(Product product in products) table.AddRow(product.Name, $"{product.Units_Sold}", FormatCurrency(product.Profit));
        AnsiConsole.Write(table);
    }

    private static string FormatCurrency(double num)
    {
        return "$ " + $"{num:n2}";
    }
}