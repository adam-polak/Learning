namespace DataAccessLibrary;

public class CardController
{
    public readonly string? stack_name;
    private ValidConnection connection;

    public CardController(string name, ValidConnection valid) 
    {
        //verify name, if not valid make null
        stack_name = name;

        connection = valid;
    }

    public void Insert(Card c)
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

    public List<Card>? Read()
    {
        if(stack_name == null) return null;
        return new List<Card>();
    }
}