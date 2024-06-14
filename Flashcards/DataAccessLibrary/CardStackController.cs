using Dapper;
using Npgsql;

namespace DataAccessLibrary;

public static class CardStackController
{

    public static void Insert(string name, ValidConnection validConnection)
    {
        if(Contains(name, validConnection)) return;
        NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO " + ValidConnection.TableNames.ElementAt(0) + " (name) VALUES (@n);", validConnection.GetConnection());
        cmd.Parameters.AddWithValue("n", name);
        cmd.ExecuteNonQuery();
    }

    public static void Update(string old, string name, ValidConnection validConnection)
    {
        if(!Contains(old, validConnection)) return;
        if(Contains(name, validConnection)) return;
        NpgsqlCommand cmd = new NpgsqlCommand("UPDATE " + ValidConnection.TableNames.ElementAt(0) + " SET name=@n WHERE name=@o;", validConnection.GetConnection());
        cmd.Parameters.AddWithValue("o", old);
        cmd.Parameters.AddWithValue("n", name);
        cmd.ExecuteNonQuery();
    }

    public static void Delete(string name, ValidConnection validConnection)
    {
        if(!Contains(name, validConnection)) return;
        NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM " + ValidConnection.TableNames.ElementAt(0) + " WHERE name=@n;", validConnection.GetConnection());
        cmd.Parameters.AddWithValue("n", name);
        cmd.ExecuteNonQuery();
    }

    public static List<CardStack> Read(ValidConnection validConnection)
    {
        return (List<CardStack>)validConnection.GetConnection().Query<CardStack>("SELECT * FROM " + ValidConnection.TableNames.ElementAt(0) + ";");
    }

    public static bool Contains(string name, ValidConnection validConnection) 
    {
        List<CardStack> list = Read(validConnection);
        foreach(CardStack cardStack in list) if(name.Equals(cardStack.Name)) return true;
        return false;
    }
}