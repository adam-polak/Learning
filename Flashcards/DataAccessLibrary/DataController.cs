using System.Data;
using Dapper;
using Npgsql;

namespace DataAccessLibrary;

public class DataController
{

    public readonly string type;
    public readonly Table table;
    private NpgsqlConnection connection;
    private static string datname = "flashcard_db";
    private static List<string> tableNames = ["stack_table", "card_table", "score_table"];
    private static string connectionString = "Host=localhost:5432;Username=postgres;Password=password;";



    public DataController(string type, Table table) 
    {
        this.type = type;
        this.table = table;
        connection = new NpgsqlConnection(connectionString);

        //if db doesn't exist create db
        if(!ContainsFlashCardDatabase()) CreateFlashCardDatabase();
        connection.ChangeDatabase(datname);
        
        for(int i = 0; i < tableNames.Count(); i++) if(!ContainsTable(i)) CreateTable(i);


        //if tables don't exist create required tables
    }

    public void Close()
    {
        connection.Close();
    }

    public void InsertStack() 
    {
        if(table != Table.Stacks || connection.State == ConnectionState.Closed) return;
        //create entry in stack table
        NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO stack_table (stack) VALUES (@t);", connection);
        cmd.Parameters.AddWithValue("t", type);
        cmd.ExecuteNonQuery();
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

    private bool ContainsFlashCardDatabase() 
    {
        List<DatabaseName> list = (List<DatabaseName>)connection.Query<DatabaseName>("SELECT datname FROM pg_database;");
        foreach(DatabaseName d in list) if(datname.Equals(d.Datname)) return true;
        return false;
    }

    private void CreateFlashCardDatabase() 
    {
        NpgsqlCommand cmd = new NpgsqlCommand("CREATE DATABASE " + datname + ";", connection);
        cmd.ExecuteNonQuery();
    }

    private bool ContainsTable(int index)
    {
        List<TableName> list = (List<TableName>)connection.Query<TableName>("SELECT table_name FROM " + datname + ".INFORMATION_SCHEMA.TABLES WHERE table_type='BASE TABLE';");
        foreach(TableName t in list) if(tableNames.ElementAt(index).Equals(t.Table_Name)) return true;
        return false;
    }

    private void CreateTable(int index) 
    {
        string command = "CREATE TABLE " + tableNames.ElementAt(index);
        if(index == 0) command += " (name TEXT PRIMARY KEY);";
        else if(index == 1) command += "(name TEXT, id INTEGER, front TEXT, back TEXT,";
        else command += "(name TEXT, id INTEGER, date TEXT, score TEXT,";

        if(index != 0) command += " FOREIGN KEY (name) REFERENCES " + tableNames.ElementAt(0) + "(name));";
        NpgsqlCommand cmd = new NpgsqlCommand(command, connection);
        cmd.ExecuteNonQuery();
    }

}

public enum Table 
{
    Stacks,
    Cards,
    Scores
}