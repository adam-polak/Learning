using DataAccessLibrary;
using Npgsql;

Console.WriteLine("Hello, World!");

ValidConnection valid = new ValidConnection();
PopulateTables.Run(valid);
CardController cardController = new CardController(valid);

List<Card>? cards = cardController.Read("Marine Science");
