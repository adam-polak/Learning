namespace DataAccessLibrary;

public class CardScoreController
{
    private readonly string? stack_name;
    private ValidConnection connection;

    public CardScoreController(string name, ValidConnection valid)
    {
        if(CardStackController.Contains(name, valid)) stack_name = name;
        else stack_name = null;
        
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