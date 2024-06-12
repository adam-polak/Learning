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
        List<Card>? cards = Read(stack_name);
        c.Id = cards != null ? cards.Count() + 1 : 1;
        c.Name = stack_name;
        NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO " + ValidConnection.TableNames.ElementAt(1) + " (name, id, front, back) VALUES (@n, @i, @f, @b);", connection.GetConnection());
        cmd.Parameters.AddWithValue("n", c.Name);
        cmd.Parameters.AddWithValue("i", c.Id);
        cmd.Parameters.AddWithValue("f", c.Front == null ? "" : c.Front);
        cmd.Parameters.AddWithValue("b", c.Back == null ? "" : c.Back);
        cmd.ExecuteNonQuery();
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
        return (List<Card>)connection.GetConnection().Query<Card>("SELECT * FROM card_table WHERE name=" + stack_name + " ORDER BY id;");
    }
}