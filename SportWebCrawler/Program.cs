using SportWebCrawler.Controllers;
using SportWebCrawler.Models;

WebController webController = new WebController();
webController.GetGamesOnDate("3/15/2024");
List<Game>? games = webController.GetGamesOnDate("3/15/2024");
if(games != null)
{
    foreach(Game game in games)
    {
        Console.WriteLine($"{game.Date} \n\t{game.Winner}: {game.Winner_Score}\n\t{game.Loser}: {game.Loser_Score}");
    }
}