using HtmlAgilityPack;

namespace SportWebCrawler.Controllers;

public class WebController
{
    private static string HtmlLink = @"https://www.basketball-reference.com/boxscores/";
    HtmlWeb web;
    HtmlDocument htmlDoc;

    public WebController()
    {
        web = new HtmlWeb();
        htmlDoc = web.Load(HtmlLink);
    }
}