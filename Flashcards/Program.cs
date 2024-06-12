using DataAccessLibrary;

Console.WriteLine("Hello, World!");

ValidConnection connection = new ValidConnection();
PopulateTables populate = new PopulateTables(connection);
