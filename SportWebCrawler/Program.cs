using SportWebCrawler.Controllers;
using SportWebCrawler.Models;

Console.WriteLine("Enter the date you would like to check for games played: (Format as mm/dd/yyyy)");
WebController webController = new WebController();
List<Game>? games = webController.GetGamesOnDate(Console.ReadLine() ?? "");
if(games != null)
{
    if(games.Count == 0) Console.WriteLine("There was no games played on this day!");
    foreach(Game game in games)
    {
        Console.WriteLine($"{game.Date} \n\t{game.Winner}: {game.Winner_Score}\n\t{game.Loser}: {game.Loser_Score}");
    }
} else Console.WriteLine("Invalid date format...");