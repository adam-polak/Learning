namespace DataAccessLibrary;

public class CardScoreController
{
    private ValidConnection connection;

    public CardScoreController(ValidConnection valid)
    {
        connection = valid;
    }

    public void Insert(CardScore cardScore, string stack_name)
    {
        if(!CardStackController.Contains(stack_name, connection)) return;
    }

    public void Update(int id, string stack_name)
    {
        if(!CardStackController.Contains(stack_name, connection)) return;
    }

    public void Delete(int id, string stack_name)
    {
        if(!CardStackController.Contains(stack_name, connection)) return;
    }

    public List<CardScore> Read(string stack_name)
    {
        if(!CardStackController.Contains(stack_name, connection)) return new List<CardScore>();
        return new List<CardScore>();
    }
}