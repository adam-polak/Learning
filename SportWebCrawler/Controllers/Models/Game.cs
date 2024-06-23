namespace SportWebCrawler.Models;

public class Game 
{
    public int Id { get; set; }
    public required string Date { get; set; }
    public required string Winner { get; set; }
    public required string Loser { get; set; }
    public int Winner_Score { get; set; }
    public int Loser_Score { get; set; }
}