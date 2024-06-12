namespace DataAccessLibrary;

public class CardScoreController
{
    private readonly string stack_name;
    private ValidConnection connection;

    public CardScoreController(string name, ValidConnection valid)
    {
        stack_name = name;
        connection = valid;
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