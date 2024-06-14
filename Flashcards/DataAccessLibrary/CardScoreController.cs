using Dapper;
using Npgsql;

namespace DataAccessLibrary;

public static class CardScoreController
{

    public static void Insert(CardScore cardScore, ValidConnection validConnection)
    {
        if(!CardStackController.Contains(cardScore.Name, validConnection)) return;
        List<CardScore> list = Read(cardScore.Name, validConnection);
        cardScore.Id = list.Count() + 1;
        NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO " + ValidConnection.TableNames.ElementAt(2) + " (name, id, date, score) VALUES (@n, @i, @d, @s);", validConnection.GetConnection());
        cmd.Parameters.AddWithValue("n", cardScore.Name);
        cmd.Parameters.AddWithValue("i", cardScore.Id);
        cmd.Parameters.AddWithValue("d", cardScore.Date);
        cmd.Parameters.AddWithValue("s", cardScore.Score);
        cmd.ExecuteNonQuery();
    }

    public static List<CardScore> Read(string stack_name, ValidConnection validConnection)
    {
        if(!CardStackController.Contains(stack_name, validConnection)) return new List<CardScore>();
        return (List<CardScore>)validConnection.GetConnection().Query<CardScore>("SELECT * FROM " + ValidConnection.TableNames.ElementAt(2) + " WHERE name='" + stack_name + "';");
    }
}