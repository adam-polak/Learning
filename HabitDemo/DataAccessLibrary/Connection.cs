using Dapper;
using Npgsql;

namespace DataAccessLibrary;

class Connection {

    private static string connectionString = "Host=localhost:5432;Username=postgres;Password=password;";
    private static string database_name = "habitdb";
    private static string table_name = "habit_table";
    private static string column_name = "glasses_of_water_per_day";
    private NpgsqlConnection connection;

    public Connection() {
        connection = new NpgsqlConnection(connectionString + "Include Error Detail=true;");
        connection.Open();

        if(!ContainsHabitDatabase()) CreateHabitDatabase();
        connection.ChangeDatabase(database_name);

        if(!ContainsHabitTable()) CreateHabitTable();
    }

    public void Close() {
        connection.Close();
    }

    public void InsertGlassesOfWater(int x) {
        NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO " + table_name + " (" + column_name + ") VALUES(@x)", connection);
        cmd.Parameters.AddWithValue("x", x);
        cmd.ExecuteNonQuery();
    }

    private void CreateHabitDatabase() {
        NpgsqlCommand cmd = new NpgsqlCommand("CREATE DATABASE " + database_name + ";", connection);
        cmd.ExecuteNonQuery();
    }

    private void CreateHabitTable() {
        NpgsqlCommand cmd = new NpgsqlCommand("CREATE TABLE " + table_name + "(glasses_of_water_per_day INTEGER)" + ";", connection);
        cmd.ExecuteNonQuery();
    }

    private bool ContainsHabitTable() {
        List<TableNames> list = (List<TableNames>)connection.Query<TableNames>("SELECT table_name FROM habitdb.INFORMATION_SCHEMA.TABLES WHERE table_type='BASE TABLE';");
        foreach(TableNames x in list) {
            if(table_name.Equals(x.Table_Name)) return true;
        }
        return false;
    }

    private bool ContainsHabitDatabase() {
        List<DatabaseNames> list = (List<DatabaseNames>)connection.Query<DatabaseNames>("SELECT datname FROM pg_database;");
        foreach(DatabaseNames x in list) {
            if(database_name.Equals(x.Datname)) return true;
        }
        return false;
    }

}