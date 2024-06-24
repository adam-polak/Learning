using SportWebCrawler.Controllers;
using SportWebCrawler.Models;
string dash = "--------------------------";
Console.WriteLine("Enter the date you would like to check for games played: (Format as mm/dd/yyyy)");
WebController webController = new WebController();
string date = Console.ReadLine() ?? "";
List<Game>? games = webController.GetGamesOnDate(date);
if(games != null)
{
    Console.WriteLine($"\n{dash}");
    Console.WriteLine("Games played on " + date);
    Console.WriteLine(dash);
    if(games.Count == 0) Console.WriteLine("There was no games played on this day!");
    else {
        Console.WriteLine("\n");
        foreach(Game game in games)
        {
            Console.WriteLine($"\t{game.Winner}: {game.Winner_Score}\n\t{game.Loser}: {game.Loser_Score}\n");
        }
        Console.WriteLine(dash);
    }
} else Console.WriteLine("Invalid date format...");