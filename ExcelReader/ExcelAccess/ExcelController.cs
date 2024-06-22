using System.Numerics;
using ExcelReader.ExcelAccess.Models;
using OfficeOpenXml;

namespace ExcelReader.ExcelAccess;

public class ExcelController
{
    private ExcelPackage package;
    private static string FilePath = @"/Users/Shared/Excel/Financial Sample.xlsx";

    public ExcelController()
    {
        Console.WriteLine("Creating excel conneciton");
        package = new ExcelPackage(new FileInfo(FilePath));
    }

    public async Task<List<Product>> LoadExcelFile()
    {
        Console.WriteLine("Loading excel data");
        List<Product> output = new List<Product>();
        await package.LoadAsync(FilePath);
        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
        int row = 2;
        int[] columns = [3, 5, 12];
        while(string.IsNullOrWhiteSpace(worksheet.Cells[row, columns[0]].Value?.ToString()) == false)
        {
            string name = worksheet.Cells[row, columns[0]].Value.ToString() ?? "";
            int units_sold = 0;
            try 
            {
                units_sold = Int32.Parse(worksheet.Cells[row, columns[1]].Value.ToString() ?? "");
            } catch(FormatException) {
                units_sold = -1;
            }
            double profit = 0.0;
            try
            {
                profit = Double.Parse(worksheet.Cells[row, columns[2]].Value.ToString() ?? "");
            } catch(FormatException) {
                profit = -1.0;
            }
            Product product = new Product() { Name = name, Units_Sold = units_sold, Profit = profit };
            output.Add(product);
            row++;
        }
        return output;
    }
}