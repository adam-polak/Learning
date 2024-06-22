namespace ExcelReader.ExcelAccess.Models;

public class Product
{
    public required string Name { get; set; }
    public int Units_Sold { get; set; }
    public double Profit { get; set; }
}