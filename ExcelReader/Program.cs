using ExcelReader;
using ExcelReader.DataAccess;
using ExcelReader.ExcelAccess;
using ExcelReader.ExcelAccess.Models;

ExcelController excelController = new ExcelController();
DataController dataController = new DataController();
List<Product> products = await excelController.LoadExcelFile();
dataController.InsertProducts(products);
products = dataController.GetProducts();
Print.PrintTable(products);