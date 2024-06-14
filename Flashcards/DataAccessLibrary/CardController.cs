using Dapper;
using Npgsql;

namespace DataAccessLibrary;

public static class CardController
{

    public static void Insert(Card c, ValidConnection validConnection)
    {
        if(!CardStackController.Contains(c.Name, validConnection)) return;
        if(ContainsFront(c.Front, c.Name, validConnection)) return;
        List<Card> cards = Read(c.Name, validConnection);
        c.Id = cards.Count() + 1;
        NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO " + ValidConnection.TableNames.ElementAt(1) + " (name, id, front, back) VALUES (@n, @i, @f, @b);", validConnection.GetConnection());
        cmd.Parameters.AddWithValue("n", c.Name);
        cmd.Parameters.AddWithValue("i", c.Id);
        cmd.Parameters.AddWithValue("f", c.Front);
        cmd.Parameters.AddWithValue("b", c.Back);
        cmd.ExecuteNonQuery();
        return;
    }

    public static bool ContainsFront(string front, string stack_name, ValidConnection validConnection)
    {
        List<Card> cards = Read(stack_name, validConnection);
        foreach(Card c in cards) if(front.Equals(c.Front)) return true;
        return false;
    }

    private static bool ContainsId(int id, string stack_name, ValidConnection validConnection)
    {
        List<Card> cards = Read(stack_name, validConnection);
        foreach(Card c in cards) if(id == c.Id) return true;
        return false;
    }

    public static void Update(Card c, ValidConnection validConnection)
    {
        if(!CardStackController.Contains(c.Name, validConnection)) return;
        if(!ContainsId(c.Id, c.Name, validConnection)) return;
        NpgsqlCommand cmd = new NpgsqlCommand("UPDATE " + ValidConnection.TableNames.ElementAt(1) + " SET front=@f, back=@b WHERE name=@n AND id=@i;", validConnection.GetConnection());
        cmd.Parameters.AddWithValue("f", c.Front);
        cmd.Parameters.AddWithValue("b", c.Back);
        cmd.Parameters.AddWithValue("n", c.Name);
        cmd.Parameters.AddWithValue("i", c.Id);
        cmd.ExecuteNonQuery();
    }

    public static void Delete(int id, string stack_name, ValidConnection validConnection)
    {
        if(!CardStackController.Contains(stack_name, validConnection)) return;
        if(!ContainsId(id, stack_name, validConnection)) return;
        NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM " + ValidConnection.TableNames.ElementAt(1) + " WHERE name=@n AND id=@i;", validConnection.GetConnection());
        cmd.Parameters.AddWithValue("n", stack_name);
        cmd.Parameters.AddWithValue("i", id);
        cmd.ExecuteNonQuery();
        cmd = new NpgsqlCommand("UPDATE " + ValidConnection.TableNames.ElementAt(1) + " SET id=id - 1 WHERE name=@n AND id>@i;", validConnection.GetConnection());
        cmd.Parameters.AddWithValue("n", stack_name);
        cmd.Parameters.AddWithValue("i", id);
        cmd.ExecuteNonQuery();
    }

    public static List<Card> Read(string stack_name, ValidConnection validConnection)
    {
        if(!CardStackController.Contains(stack_name, validConnection)) return new List<Card>();
        return (List<Card>)validConnection.GetConnection().Query<Card>("SELECT * FROM " + ValidConnection.TableNames.ElementAt(1) + " WHERE name='" + stack_name + "' ORDER BY id;");
    }
}