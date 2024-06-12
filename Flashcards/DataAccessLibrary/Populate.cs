namespace DataAccessLibrary;

public class PopulateTables
{
    private ValidConnection connection;
    private CardStackController cardStackController;
    private CardController cardController;
    private CardScoreController cardScoreController;

    private List<string> stack_names = ["Marine Science", "Animal Noises"];
    private List<string> MarineFrontCard = ["Whales live in the _____", "Beaches have ______", "Turtles lay eggs in the ______"];
    private List<string> MarineBackCard = ["ocean", "sand", "sand"];
    private List<string> AnimalFrontCard = ["Cows go ____", "Dogs go ____", "Cats go ____"];
    private List<string> AnimalBackCard = ["moo", "woof", "meow"];

    public PopulateTables(ValidConnection valid)
    {
        connection = valid;
        cardStackController = new CardStackController(connection);
        PopulateStackTable();
        cardController = new CardController(connection);
        PopulateCardTable();
        cardScoreController = new CardScoreController(connection);
    }

    public void PopulateCardTable()
    {
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

    public void PopulateScoreTable()
    {

    }

    public void PopulateStackTable()
    {
        foreach(string s in stack_names) cardStackController.Insert(s);
    }
}