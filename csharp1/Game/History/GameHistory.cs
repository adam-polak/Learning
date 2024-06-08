namespace CSharp1;

public static class GameHistory {
    private static List<Game> Games = new List<Game>();

    public static List<Game> GetGames() {
        return Games;
    }

    public static void AddGame(Game g) {
        Games.Add(g);
    }
}