namespace DataAccessLibrary;

public class CardStackController
{

    private ValidConnection connection;

    public CardStackController(ValidConnection valid)
    {
        connection = valid;
    }

    public void Insert(string name, ValidConnection valid)
    {
        
    }

    public void Update()
    {

    }

    public void Delete()
    {

    }

    public List<CardStack> Read()
    {
        return new List<CardStack>();
    }
}