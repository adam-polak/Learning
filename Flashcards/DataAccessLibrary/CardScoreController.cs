namespace DataAccessLibrary;

public class CardScoreController
{
    private readonly string stack_name;

    public CardScoreController(string name)
    {
        stack_name = name;
    }

    public void Insert(CardScore cardScore)
    {

    }

    public void Update(int id)
    {

    }

    public void Delete(int id)
    {

    }

    public List<CardScore> Read()
    {
        return new List<CardScore>();
    }
}