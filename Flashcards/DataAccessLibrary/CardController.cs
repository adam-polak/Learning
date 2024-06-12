namespace DataAccessLibrary;

public class CardController
{
    private ValidConnection connection;

    public CardController(ValidConnection valid) 
    {
        connection = valid;
    }

    public void Insert(Card c, string stack_name)
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

    public List<Card>? Read(string stack_name)
    {
        if(!CardStackController.Contains(stack_name, connection)) return null;
        return new List<Card>();
    }
}