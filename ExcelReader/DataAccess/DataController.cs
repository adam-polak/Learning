using ExcelReader.DataAccess.Models;
using Npgsql;
using Dapper;

namespace ExcelReader.DataAccess;

public class DataController
{
    private NpgsqlConnection connection;
    private static string database_name = "financial_sample_excel";
    private static string table_name = "product_table";
    private static string ConnectionString = "Host=localhost:5432;Username=postgres;Password=password;";


    public DataController()
    {
        connection = new NpgsqlConnection(ConnectionString);
        ConnectToDatabase();
        if(!ContainsTable()) CreateTable();
    }

    private void ConnectToDatabase()
    {
        if(!ContainsDatabase()) CreateDatabase();
        connection.ChangeDatabase(database_name);
    }

    private void CreateTable()
    {
        NpgsqlCommand cmd = new NpgsqlCommand($"CREATE TABLE {table_name} (name TEXT, units_sold INTEGER, profit DOUBLE);", connection);
        cmd.ExecuteNonQuery();
    }

    private void CreateDatabase()
    {
        NpgsqlCommand cmd = new NpgsqlCommand($"CREATE DATABASE {database_name};", connection);
        cmd.ExecuteNonQuery();
    }

    private bool ContainsDatabase()
    {
        List<DatabaseName> list = (List<DatabaseName>)connection.Query<DatabaseName>("SELECT datname FROM pg_database;");
        foreach(DatabaseName x in list) {
            if(database_name.Equals(x.Datname)) return true;
        }
        return false;
    }

    private bool ContainsTable()
    {
        List<TableName> list = (List<TableName>)connection.Query<TableName>("SELECT table_name FROM habitdb.INFORMATION_SCHEMA.TABLES WHERE table_type='BASE TABLE';");
        foreach(TableName x in list) {
            if(table_name.Equals(x.Table_Name)) return true;
        }
        return false;
    }
}