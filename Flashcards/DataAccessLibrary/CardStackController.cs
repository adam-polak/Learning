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

    public static List<CardStack> Read(ValidConnection valid)
    {
        return new List<CardStack>();
    }

    public static bool Contains(CardStack cardStack, ValidConnection valid) {
        if(cardStack.Name == null) return false;
        List<CardStack> list = Read(valid);
        foreach(CardStack c in list) if(cardStack.Name.Equals(c.Name)) return true;
        return false;
    }
}