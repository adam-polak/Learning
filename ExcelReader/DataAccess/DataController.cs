using ExcelReader.DataAccess.Models;
using ExcelReader.ExcelAccess.Models;
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
        connection.Open();
        ConnectToDatabase();
        if(!ContainsTable()) CreateTable();
        connection.Close();
    }

    public List<Product> GetProducts()
    {
        connection.Open();
        string queryString = $"SELECT name, SUM(units_sold), SUM(profit) FROM {table_name} "
                            + "WHERE units_sold > 0 AND profit > 0 "
                            + "GROUP BY name;";
        List<Product> products = (List<Product>)connection.Query<Product>(queryString);
        connection.Close();
        return products;
    }

    public void InsertProducts(List<Product> products)
    {
        connection.Open();
        ClearTable();
        NpgsqlCommand cmd;
        foreach(Product product in products)
        {
            cmd = new NpgsqlCommand($"INSERT INTO {table_name} (name, units_sold, profit) VALUES (@n, @u, @p);", connection);
            cmd.Parameters.AddWithValue("n", product.Name);
            cmd.Parameters.AddWithValue("u", product.Units_Sold);
            cmd.Parameters.AddWithValue("p", product.Profit);
            cmd.ExecuteNonQuery();
        }
        connection.Close();
    }

    private void ClearTable()
    {
        NpgsqlCommand cmd = new NpgsqlCommand($"DELETE FROM {table_name};", connection);
        cmd.ExecuteNonQuery();
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
        List<TableName> list = (List<TableName>)connection.Query<TableName>($"SELECT table_name FROM {database_name}.INFORMATION_SCHEMA.TABLES WHERE table_type='BASE TABLE';");
        foreach(TableName x in list) {
            if(table_name.Equals(x.Table_Name)) return true;
        }
        return false;
    }
}