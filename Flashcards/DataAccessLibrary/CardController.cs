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

    public void Insert(Card c, string stack_name)
    {
        if(!CardStackController.Contains(stack_name, connection)) return;
        if(ContainsFront(c.Front, stack_name)) return;
        List<Card> cards = Read(stack_name);
        c.Id = cards.Count() + 1;
        c.Name = stack_name;
        NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO " + ValidConnection.TableNames.ElementAt(1) + " (name, id, front, back) VALUES (@n, @i, @f, @b);", connection.GetConnection());
        cmd.Parameters.AddWithValue("n", c.Name);
        cmd.Parameters.AddWithValue("i", c.Id);
        cmd.Parameters.AddWithValue("f", c.Front);
        cmd.Parameters.AddWithValue("b", c.Back);
        cmd.ExecuteNonQuery();
        return;
    }

    public bool ContainsFront(string front, string stack_name)
    {
        List<Card> cards = Read(stack_name);
        foreach(Card c in cards) if(front.Equals(c.Front)) return true;
        return false;
    }

    private bool ContainsId(int id, string stack_name)
    {
        List<Card> cards = Read(stack_name);
        foreach(Card c in cards) if(id == c.Id) return true;
        return false;
    }

    public void Update(Card c, string stack_name)
    {
        if(!CardStackController.Contains(stack_name, connection)) return;
        if(!ContainsId(c.Id, stack_name)) return;
        NpgsqlCommand cmd = new NpgsqlCommand("UPDATE " + ValidConnection.TableNames.ElementAt(1) + " SET front=@f, back=@b WHERE name=@n AND id=@i;", connection.GetConnection());
        cmd.Parameters.AddWithValue("f", c.Front);
        cmd.Parameters.AddWithValue("b", c.Back);
        cmd.Parameters.AddWithValue("n", stack_name);
        cmd.Parameters.AddWithValue("i", c.Id);
        cmd.ExecuteNonQuery();
    }

    public void Delete(int id, string stack_name)
    {
        if(!CardStackController.Contains(stack_name, connection)) return;
        if(!ContainsId(id, stack_name)) return;
        NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM " + ValidConnection.TableNames.ElementAt(1) + " WHERE name=@n AND id=@i;", connection.GetConnection());
        cmd.Parameters.AddWithValue("n", stack_name);
        cmd.Parameters.AddWithValue("i", id);
        cmd.ExecuteNonQuery();
        cmd = new NpgsqlCommand("UPDATE " + ValidConnection.TableNames.ElementAt(1) + " SET id=id - 1 WHERE name=@n AND id>@i;", connection.GetConnection());
        cmd.Parameters.AddWithValue("n", stack_name);
        cmd.Parameters.AddWithValue("i", id);
        cmd.ExecuteNonQuery();
    }

    public List<Card> Read(string stack_name)
    {
        if(!CardStackController.Contains(stack_name, connection)) return new List<Card>();
        return (List<Card>)connection.GetConnection().Query<Card>("SELECT * FROM " + ValidConnection.TableNames.ElementAt(1) + " WHERE name='" + stack_name + "' ORDER BY id;");
    }
}