using Dapper;
using Npgsql;

namespace DataAccessLibrary;

public class CardScoreController
{
    private ValidConnection connection;

    public CardScoreController(ValidConnection valid)
    {
        connection = valid;
    }

    public void Insert(CardScore cardScore)
    {
        if(!CardStackController.Contains(cardScore.Name, connection)) return;
        List<CardScore> list = Read(cardScore.Name);
        cardScore.Id = list.Count() + 1;
        NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO " + ValidConnection.TableNames.ElementAt(2) + " (name, id, date, score) VALUES (@n, @i, @d, @s);", connection.GetConnection());
        cmd.Parameters.AddWithValue("n", cardScore.Name);
        cmd.Parameters.AddWithValue("i", cardScore.Id);
        cmd.Parameters.AddWithValue("d", cardScore.Date);
        cmd.Parameters.AddWithValue("s", cardScore.Score);
        cmd.ExecuteNonQuery();
    }

    //Shouldn't need update/delete methods for cardscore controller

    // public void Update(int id, string stack_name)
    // {
    //     if(!CardStackController.Contains(stack_name, connection)) return;
    // }

    // public void Delete(int id, string stack_name)
    // {
    //     if(!CardStackController.Contains(stack_name, connection)) return;
    // }

    public List<CardScore> Read(string stack_name)
    {
        if(!CardStackController.Contains(stack_name, connection)) return new List<CardScore>();
        return (List<CardScore>)connection.GetConnection().Query<CardScore>("SELECT * FROM " + ValidConnection.TableNames.ElementAt(2) + " WHERE name=" + stack_name + ";");
    }
}