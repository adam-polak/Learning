using DataAccessLibrary;
using UILogic;

Console.WriteLine("Hello, World!");

PopulateTables.Run(new ValidConnection());
Driver d = new Driver();
d.Run();