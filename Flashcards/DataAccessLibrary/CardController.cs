using Dapper;
using Npgsql;

namespace DataAccessLibrary;

public class CardController
{
    private ValidConnection connection;

    public CardController(ValidConnection valid) 
    {
        connection = valid;
    }

    public bool Insert(Card c, string stack_name)
    {
        if(!CardStackController.Contains(stack_name, connection)) return false;
        if(c.Front == null || c.Back == null) return false;
        if(ContainsFront(c.Front, stack_name)) return false;
        List<Card>? cards = Read(stack_name);
        c.Id = cards != null ? cards.Count() + 1 : 1;
        c.Name = stack_name;
        NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO " + ValidConnection.TableNames.ElementAt(1) + " (name, id, front, back) VALUES (@n, @i, @f, @b);", connection.GetConnection());
        cmd.Parameters.AddWithValue("n", c.Name);
        cmd.Parameters.AddWithValue("i", c.Id);
        cmd.Parameters.AddWithValue("f", c.Front);
        cmd.Parameters.AddWithValue("b", c.Back);
        cmd.ExecuteNonQuery();
        return true;
    }

    private bool ContainsFront(string front, string stack_name)
    {
        List<Card>? cards = Read(stack_name);
        if(cards == null) return false;
        foreach(Card c in cards) if(front.Equals(c.Front)) return true;
        return false;
    }

    public void Update(Card c, string stack_name)
    {
        if(!CardStackController.Contains(stack_name, connection)) return;
        List<Card>? cards = Read(stack_name);
        if(cards == null) return;
        bool containsCard = false;
        foreach(Card x in cards) if(c.Id == x.Id) containsCard = true;
        if(!containsCard || c.Front == null || c.Back == null) return;
        NpgsqlCommand cmd = new NpgsqlCommand("UPDATE " + ValidConnection.TableNames.ElementAt(1) + " SET front=@f, back=@b WHERE name=@n AND id=@i;");
        cmd.Parameters.AddWithValue("f", c.Front);
        cmd.Parameters.AddWithValue("b", c.Back);
        cmd.Parameters.AddWithValue("n", stack_name);
        cmd.Parameters.AddWithValue("i", c.Id);
        cmd.ExecuteNonQuery();

    }

    public void Delete(int id, string stack_name)
    {
        if(!CardStackController.Contains(stack_name, connection)) return;
    }

    public List<Card>? Read(string stack_name)
    {
        if(!CardStackController.Contains(stack_name, connection)) return null;
        return (List<Card>)connection.GetConnection().Query<Card>("SELECT * FROM " + ValidConnection.TableNames.ElementAt(1) + " WHERE name='" + stack_name + "' ORDER BY id;");
    }
}