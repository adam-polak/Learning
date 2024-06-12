namespace DataAccessLibrary;

public class CardScoreController
{
    private readonly string? stack_name;
    private ValidConnection connection;

    public CardScoreController(string name, ValidConnection valid)
    {
        stack_name = name;
        connection = valid;
    }

    public void Insert(CardScore cardScore)
    {
        if(stack_name == null) return;
    }

    public void Update(int id)
    {
        if(stack_name == null) return;
    }

    public void Delete(int id)
    {
        if(stack_name == null) return;
    }

    public List<CardScore>? Read()
    {
        if(stack_name == null) return null;
        return new List<CardScore>();
    }
}