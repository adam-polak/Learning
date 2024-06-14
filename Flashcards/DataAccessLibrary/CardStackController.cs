using Dapper;
using Npgsql;

namespace DataAccessLibrary;

public static class CardStackController
{

    public static void Insert(string name, ValidConnection valid)
    {
        if(Contains(name, valid)) return;
        NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO " + ValidConnection.TableNames.ElementAt(0) + " (name) VALUES (@n);", valid.GetConnection());
        cmd.Parameters.AddWithValue("n", name);
        cmd.ExecuteNonQuery();
    }

    public static void Update(string old, string name, ValidConnection valid)
    {
        if(!Contains(old, valid)) return;
        if(Contains(name, valid)) return;
        NpgsqlCommand cmd = new NpgsqlCommand("UPDATE " + ValidConnection.TableNames.ElementAt(0) + " SET name=@n WHERE name=@o;", valid.GetConnection());
        cmd.Parameters.AddWithValue("o", old);
        cmd.Parameters.AddWithValue("n", name);
        cmd.ExecuteNonQuery();
    }

    public static void Delete(string name, ValidConnection valid)
    {
        if(!Contains(name, valid)) return;
        NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM " + ValidConnection.TableNames.ElementAt(0) + " WHERE name=@n;", valid.GetConnection());
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