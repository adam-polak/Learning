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
        List<HtmlNodeCollection> nodeList = FindGamesDiv(htmlDoc.DocumentNode.SelectNodes("//div/table"));
        foreach(HtmlNodeCollection nodes in nodeList) games.Add(MakeGame(nodes, date));
        return games;
    }

    private Game MakeGame(HtmlNodeCollection htmlNodes, string date)
    {
        string winner = "";
        string loser = "";
        int winner_score = 0;
        int loser_score = 0;
        foreach(HtmlNode node in htmlNodes)
        {
            if(node.HasClass("winner"))
            {
                foreach(HtmlNode child in node.ChildNodes)
                {
                    if(child.ChildNodes.Count == 1) winner = child.ChildNodes.First().InnerHtml;
                    else Int32.TryParse(child.InnerHtml, out winner_score);
                }
            } else {
                foreach(HtmlNode child in node.ChildNodes)
                {
                    if(child.ChildNodes.Count == 1) loser = child.ChildNodes.First().InnerHtml;
                    else Int32.TryParse(child.InnerHtml, out loser_score);
                }
            }
        }
        return new Game() { Date = date, Winner = winner, Loser = loser, Winner_Score = winner_score, Loser_Score = loser_score };
    }

    private List<HtmlNodeCollection> FindGamesDiv(HtmlNodeCollection htmlNodes)
    {
        List<HtmlNodeCollection> list = new List<HtmlNodeCollection>();
        foreach(HtmlNode node in htmlNodes)
        {
            if(node.HasClass("teams")) list.Add(node.ChildNodes);
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