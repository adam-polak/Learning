using DataAccessLibrary;
using Npgsql;

Console.WriteLine("Hello, World!");

ValidConnection valid = new ValidConnection();
PopulateTables.Run(valid);
