namespace DataAccessLibrary;

public class CardController
{
    public readonly string stack_name;
    private ValidConnection connection;

    public CardController(string name, ValidConnection valid) 
    {
        //verify name
        stack_name = name;

        connection = valid;
    }

    public void Insert(Card c)
    {

    }

    public void Update(int id)
    {

    }

    public void Delete(int id)
    {

    }

    public List<Card> Read()
    {
        return new List<Card>();
    }
}