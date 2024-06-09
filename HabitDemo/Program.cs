// See https://aka.ms/new-console-template for more information
using DataAccessLibrary;

Console.WriteLine("Hello, World!");

Connection c = new Connection();
c.InsertGlassesOfWater(3);
c.Close();
