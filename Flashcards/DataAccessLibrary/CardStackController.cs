using Dapper;

namespace DataAccessLibrary;

public class CardStackController
{

    private ValidConnection connection;

    public CardStackController(ValidConnection valid)
    {
        connection = valid;
    }

    public void Insert(string name)
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
        List<CardStack> ans = (List<CardStack>)valid.GetConnection().Query<CardStack>("SELECT * FROM " + ValidConnection.TableNames.ElementAt(0) + ";");
        return ans;
    }

    public static bool Contains(string name, ValidConnection valid) 
    {
        List<CardStack> list = Read(valid);
        foreach(CardStack cardStack in list) if(name.Equals(cardStack.Name)) return true;
        return false;
    }
}