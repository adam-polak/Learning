namespace DataAccessLibrary;

public class PopulateTables
{
    private ValidConnection connection;
    private CardStackController? cardStackController;
    private CardController? cardController;
    private CardScoreController? cardScoreController;

    private List<string> stack_names = ["Marine Science", "Animal Noises"];
    private List<string> MarineFrontCard = ["Whales live in the _____", "Beaches have ______", "Turtles lay eggs in the ______"];
    private List<string> MarineBackCard = ["ocean", "sand", "sand"];
    private List<string> AnimalFrontCard = ["Cows go ____", "Dogs go ____", "Cats go ____"];
    private List<string> AnimalBackCard = ["moo", "woof", "meow"];

    public PopulateTables(ValidConnection valid)
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

    private void PopulateCardTable()
    {
        if(cardController == null) return;
        string stack_name = stack_names.ElementAt(0);
        Card c = new Card();
        for(int i = 0; i < MarineFrontCard.Count(); i++) 
        {
            c.Front = MarineFrontCard.ElementAt(i);
            c.Back = MarineBackCard.ElementAt(i);
            cardController.Insert(c, stack_name);
        }
        stack_name = stack_names.ElementAt(1);
        for(int i = 0; i < AnimalFrontCard.Count(); i++)
        {
            c.Front = AnimalFrontCard.ElementAt(i);
            c.Back = AnimalBackCard.ElementAt(i);
            cardController.Insert(c, stack_name);
        }
    }

    private void PopulateScoreTable()
    {
        if(cardScoreController == null || cardController == null) return;
        CardScore cardScore = new CardScore();
        List<Card>? cards;
        Random rnd = new Random();
        foreach(string stack_name in stack_names)
        {
            cards = cardController.Read(stack_name);
            if(cards != null) 
            {
                for(int i = 0; i < 5; i++)
                {
                    cardScore.Date = DateTime.Now.ToShortDateString();
                    cardScore.Type = stack_name;
                    cardScore.Score = rnd.Next(cards.Count()) + "/" + cards.Count();
                    cardScoreController.Insert(cardScore, stack_name);
                }
            }
        }
    }

    private void PopulateStackTable()
    {
        if(cardStackController == null) return;
        foreach(string s in stack_names) cardStackController.Insert(s);
    }
}