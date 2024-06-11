using System.Data;
using Dapper;
using Npgsql;

namespace DataAccessLibrary;

public class DataController
{

    public readonly string type;
    public readonly Table table;
    private NpgsqlConnection connection;
    private static string connectionString = "Host=localhost:5432;Username=postgres;Password=password;Database=flashcard";



    public DataController(string type, Table table) 
    {
        this.type = type;
        this.table = table;
        connection = new NpgsqlConnection(connectionString);
    }

    public void Close()
    {
        connection.Close();
    }

    public void InsertStack() 
    {
        if(table != Table.Stacks || connection.State == ConnectionState.Closed) return;
        connection.Close();
    }

    public void InsertCard(string front, string back)
    {
        if(table != Table.Cards) return;
    }

    public void InsertScore(DateTime date, string score)
    {
        if(table != Table.Scores) return;
    }

    public void Update(int id) 
    {
        if(table == Table.Stacks) return;
    }

    public void Delete(string t) 
    {
        if(table != Table.Stacks) return;
    }

    public void Delete(int id) 
    {
        if(table == Table.Stacks) return;
    }

    public List<CardStack>? ReadStacks() 
    {
        if(table != Table.Stacks) return null;
        return new List<CardStack>();
    }

    public List<Card>? ReadCards() 
    {
        if(table != Table.Cards) return null;
        return new List<Card>();
    }

    public List<CardScore>? ReadScores() 
    {
        if(table != Table.Scores) return null;
        return new List<CardScore>();
    }

}

public enum Table 
{
    Stacks,
    Cards,
    Scores
}