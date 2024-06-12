using Npgsql;
using Dapper;
using System.Data;

namespace DataAccessLibrary;

public class ValidConnection
{

    private static string datname = "flashcard_db";
    public static readonly List<string> TableNames = ["stack_table", "card_table", "score_table"];
    private static string connectionString = "Host=localhost:5432;Username=postgres;Password=password;";
    private NpgsqlConnection connection;

    public ValidConnection()
    {
        connection = new NpgsqlConnection(connectionString);
        EnsureConnection();
    }

    public NpgsqlConnection GetConnection()
    {
        EnsureConnection();
        return connection;
    }

    private void EnsureConnection()
    {
        if(connection.State != ConnectionState.Open) connection.Open();

        if(!datname.Equals(connection.Database)) {
            if(!ContainsFlashCardDatabase()) CreateFlashCardDatabase();
            connection.ChangeDatabase(datname);
        }
        
        for(int i = 0; i < TableNames.Count(); i++) if(!ContainsTable(i)) CreateTable(i);
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
        foreach(TableName t in list) if(TableNames.ElementAt(index).Equals(t.Table_Name)) return true;
        return false;
    }

    private void CreateTable(int index) 
    {
        string command = "CREATE TABLE " + TableNames.ElementAt(index);
        if(index == 0) command += " (name TEXT PRIMARY KEY);";
        else if(index == 1) command += "(name TEXT, id INTEGER, front TEXT, back TEXT,";
        else command += "(name TEXT, id INTEGER, date TEXT, score TEXT,";

        if(index != 0) command += " FOREIGN KEY (name) REFERENCES " + TableNames.ElementAt(0) + "(name));";
        NpgsqlCommand cmd = new NpgsqlCommand(command, connection);
        cmd.ExecuteNonQuery();
    }

}