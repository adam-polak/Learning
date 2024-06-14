namespace DataAccessLibrary;

public static class PopulateTables
{
    private static ValidConnection? connection;

    private static List<string> stack_names = ["Marine Science", "Animal Noises"];
    private static List<string> MarineFrontCard = ["Whales live in the _____", "Beaches have ______", "Turtles lay eggs in the ______"];
    private static List<string> MarineBackCard = ["ocean", "sand", "sand"];
    private static List<string> AnimalFrontCard = ["Cows go ____", "Dogs go ____", "Cats go ____"];
    private static List<string> AnimalBackCard = ["moo", "woof", "meow"];

    public static void Run(ValidConnection valid)
    {
        connection = valid;
        if(!CardStackController.Contains(stack_names.ElementAt(0), connection)) 
        {
            PopulateStackTable();
            PopulateCardTable();
            PopulateScoreTable();
        }
    }

    private static void PopulateCardTable()
    {
        if(connection == null) return;
        string stack_name = stack_names.ElementAt(0);
        Card c;
        for(int i = 0; i < MarineFrontCard.Count(); i++) 
        {
            c = new Card() {Name = stack_name, Front = MarineFrontCard.ElementAt(i), Back = MarineBackCard.ElementAt(i)};
            CardController.Insert(c, connection);
        }
        stack_name = stack_names.ElementAt(1);
        for(int i = 0; i < AnimalFrontCard.Count(); i++)
        {
            c = new Card() {Name = stack_name, Front = AnimalFrontCard.ElementAt(i), Back = AnimalBackCard.ElementAt(i)};
            CardController.Insert(c, connection);
        }
    }

    private static void PopulateScoreTable()
    {
        if(connection == null) return;
        CardScore cardScore;
        List<Card> cards;
        Random rnd = new Random();
        foreach(string stack_name in stack_names)
        {
            cards = CardController.Read(stack_name, connection);
            if(cards != null) 
            {
                for(int i = 0; i < 5; i++)
                {
                    cardScore = new CardScore() {Name = stack_name, Date = DateTime.Now.ToShortDateString(), Score = rnd.Next(cards.Count()) + "/" + cards.Count()};
                    CardScoreController.Insert(cardScore, connection);
                }
            }
        }
    }

    private static void PopulateStackTable()
    {
        if(connection == null) return;
        foreach(string s in stack_names) CardStackController.Insert(s, connection);
    }
}