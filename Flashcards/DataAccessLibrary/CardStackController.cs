using Dapper;
using Npgsql;

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
        if(Contains(name, connection)) return;
        NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO " + ValidConnection.TableNames.ElementAt(0) + " (name) VALUES (@n);", connection.GetConnection());
        cmd.Parameters.AddWithValue("n", name);
        cmd.ExecuteNonQuery();
    }

    public void Update(string old, string name)
    {
        if(!Contains(old, connection)) return;
        if(Contains(name, connection)) return;
        NpgsqlCommand cmd = new NpgsqlCommand("UPDATE " + ValidConnection.TableNames.ElementAt(0) + " SET name=@n WHERE name=@o;", connection.GetConnection());
        cmd.Parameters.AddWithValue("o", old);
        cmd.Parameters.AddWithValue("n", name);
        cmd.ExecuteNonQuery();
    }

    public void Delete(string name)
    {
        if(!Contains(name, connection)) return;
        NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM " + ValidConnection.TableNames.ElementAt(0) + " WHERE name=@n;", connection.GetConnection());
        cmd.Parameters.AddWithValue("n", name);
        cmd.ExecuteNonQuery();
    }

    public static List<CardStack> Read(ValidConnection valid)
    {
        return (List<CardStack>)valid.GetConnection().Query<CardStack>("SELECT * FROM " + ValidConnection.TableNames.ElementAt(0) + ";");
    }

    public static bool Contains(string name, ValidConnection valid) 
    {
        List<CardStack> list = Read(valid);
        foreach(CardStack cardStack in list) if(name.Equals(cardStack.Name)) return true;
        return false;
    }
}