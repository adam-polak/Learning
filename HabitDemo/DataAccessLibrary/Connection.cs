using Dapper;
using Npgsql;

namespace DataAccessLibrary;

class Connection {

    private static string connectionString = "Host=localhost:5432;Username=postgres;Password=password;";
    private static string habitdb = "habitdb";
    private static string table_name = "water_glasses_per_day";
    private NpgsqlConnection connection;

    public Connection() {
        connection = new NpgsqlConnection(connectionString);
        connection.Open();

        if(!ContainsHabitDatabase()) CreateHabitDatabase();
        connection.ChangeDatabase(habitdb);

        if(!ContainsHabitTable()) CreateHabitTable();
    }

    private void CreateHabitDatabase() {
        NpgsqlCommand cmd = new NpgsqlCommand("CREATE DATABASE " + habitdb + ";", connection);
        cmd.ExecuteNonQuery();
    }

    private void CreateHabitTable() {
        NpgsqlCommand cmd = new NpgsqlCommand("CREATE TABLE " + table_name + ";", connection);
    }

    private bool ContainsHabitTable() {
        List<QueryStringObject> list = (List<QueryStringObject>)connection.Query<QueryStringObject>("SELECT table_name FROM habitdb.INFORMATION_SCHEMA.TABLES WHERE table_type='BASE TABLE';");
        foreach(QueryStringObject x in list) {
            if(table_name.Equals(x.Item)) return true;
        }
        return false;
    }

    private bool ContainsHabitDatabase() {
        List<QueryStringObject> list = (List<QueryStringObject>)connection.Query<QueryStringObject>("SELECT datname FROM pg_database;");
        foreach(QueryStringObject x in list) {
            if(habitdb.Equals(x.Item)) return true;
        }
        return false;
    }

}