namespace DataAccessLibrary;

public static class PopulateTables
{
    private static ValidConnection? connection;
    private static CardStackController? cardStackController;
    private static CardController? cardController;
    private static CardScoreController? cardScoreController;

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
            cardStackController = new CardStackController(connection);
            PopulateStackTable();
            cardController = new CardController(connection);
            PopulateCardTable();
            cardScoreController = new CardScoreController(connection);
            PopulateScoreTable();
        }
    }

    private static void PopulateCardTable()
    {
        if(cardController == null) return;
        string stack_name = stack_names.ElementAt(0);
        Card c;
        for(int i = 0; i < MarineFrontCard.Count(); i++) 
        {
            c = new Card() {Name = stack_name, Front = MarineFrontCard.ElementAt(i), Back = MarineBackCard.ElementAt(i)};
            cardController.Insert(c);
        }
        stack_name = stack_names.ElementAt(1);
        for(int i = 0; i < AnimalFrontCard.Count(); i++)
        {
            c = new Card() {Name = stack_name, Front = AnimalFrontCard.ElementAt(i), Back = AnimalBackCard.ElementAt(i)};
            cardController.Insert(c);
        }
    }

    private static void PopulateScoreTable()
    {
        if(cardScoreController == null || cardController == null) return;
        CardScore cardScore;
        List<Card> cards;
        Random rnd = new Random();
        foreach(string stack_name in stack_names)
        {
            cards = cardController.Read(stack_name);
            if(cards != null) 
            {
                for(int i = 0; i < 5; i++)
                {
                    cardScore = new CardScore() {Name = stack_name, Date = DateTime.Now.ToShortDateString(), Score = rnd.Next(cards.Count()) + "/" + cards.Count()};
                    cardScoreController.Insert(cardScore);
                }
            }
        }
    }

    private static void PopulateStackTable()
    {
        if(cardStackController == null) return;
        foreach(string s in stack_names) cardStackController.Insert(s);
    }
}