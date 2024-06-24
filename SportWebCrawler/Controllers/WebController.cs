using HtmlAgilityPack;
using SportWebCrawler.Models;

namespace SportWebCrawler.Controllers;

public class WebController
{
    private static string HtmlLink = @"https://www.basketball-reference.com/boxscores/";
    HtmlWeb web;

    public WebController()
    {
        web = new HtmlWeb();
    }

    public List<Game>? GetGamesOnDate(string date)
    {
        DateTime dateTime;
        if(!DateTime.TryParse(date, out dateTime)) return null;
        HtmlDocument htmlDoc = web.Load(HtmlLink + EndOfLink(dateTime));
        List<Game> games = new List<Game>();
        List<HtmlNode[]> nodePairList = FindGames(htmlDoc.DocumentNode.SelectNodes("//div/table/tbody/tr"));
        foreach(HtmlNode[] nodes in nodePairList) games.Add(MakeGame(nodes, date));
        return games;
    }

    private Game MakeGame(HtmlNode[] nodes, string date)
    {
        HtmlNodeCollection winnerChildren = nodes[0].ChildNodes;
        HtmlNodeCollection loserChildren = nodes[1].ChildNodes;
        string winner = winnerChildren.ElementAt(1).ChildNodes.ElementAt(0).InnerHtml;
        string loser = loserChildren.ElementAt(1).InnerText;
        int winner_score = Int32.Parse(winnerChildren.ElementAt(3).InnerText);
        int loser_score = Int32.Parse(loserChildren.ElementAt(3).InnerText);
        return new Game() { Date = date, Winner = winner, Loser = loser, Winner_Score = winner_score, Loser_Score = loser_score };
    }

    private List<HtmlNode[]> FindGames(HtmlNodeCollection htmlNodes)
    {
        List<HtmlNode[]> list = new List<HtmlNode[]>();
        HtmlNode[]? pair = new HtmlNode[2];
        foreach(HtmlNode node in htmlNodes)
        {
            if(node.HasClass("winner")) pair[0] = node;
            else if(node.HasClass("loser")) pair[1] = node;

            if(pair[0] != null && pair[1] != null)
            {
                list.Add(pair);
                pair = new HtmlNode[2];
            }
        }
        return list;
    }

    private string EndOfLink(DateTime date)
    {
        string formatMonth = date.Month < 10 ? $"0{date.Month}" : $"{date.Month}";
        string formatDay = date.Day < 10 ? $"0{date.Day}" : $"{date.Day}";
        return $"?month={formatMonth}&day={formatDay}&year={date.Year}";
    }
}