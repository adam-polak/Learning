namespace DataAccessLibrary;

public class CardController
{
    public readonly string stack_name;

    public CardController(string name) 
    {
        //verify name
        stack_name = name;
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