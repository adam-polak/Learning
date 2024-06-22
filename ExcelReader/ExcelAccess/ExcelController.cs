using ExcelReader.ExcelAccess.Models;
using OfficeOpenXml;

namespace ExcelReader.ExcelAccess;

public class ExcelController
{
    private ExcelPackage package;
    private static string FilePath = @"/Users/Shared/Excel/Financial Sample.xlsx";

    public ExcelController()
    {
        package = new ExcelPackage(new FileInfo(FilePath));
    }

    public async Task<List<Product>> LoadExcelFile()
    {
        List<Product> output = new List<Product>();
        await package.LoadAsync(FilePath);
        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
        int row = 2;
        int[] columns = [3, 5, 12];
        while(string.IsNullOrWhiteSpace(worksheet.Cells[row, columns[0]].Value?.ToString()) == false)
        {
            string name = worksheet.Cells[row, columns[0]].Value.ToString() ?? "";
            string units_sold = worksheet.Cells[row, columns[1]].Value.ToString() ?? "";
            string profit = worksheet.Cells[row, columns[2]].Value.ToString() ?? "";
            Product product = new Product() { Name = name, Units_Sold = units_sold, Profit = profit };
            output.Add(product);
            row++;
        }
        foreach(Product x in output) Console.WriteLine(x.Units_Sold);
        return output;
    }
}