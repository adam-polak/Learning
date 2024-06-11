using Dapper;
using Npgsql;

namespace DataAccessLibrary;

class Connection {

    private static string connectionString = "Host=localhost:5432;Username=postgres;Password=password;";
    private static string database_name = "habitdb";
    private static string table_name = "habit_table";
    private NpgsqlConnection connection;

    public Connection() {
        connection = new NpgsqlConnection(connectionString + "Include Error Detail=true;");
        connection.Open();

        if(!ContainsHabitDatabase()) CreateHabitDatabase();
        connection.ChangeDatabase(database_name);

        if(!ContainsHabitTable()) {
            CreateHabitTable();
            PopulateTable();
        }
    }

    public void Close() {
        connection.Close();
    }

    private void PopulateTable() {
        int count = 0;
        Random rnd = new Random();
        while(count < 100) {
            InsertWaterEntry(rnd.Next(11));
            count++;
        }
    }

    public void InsertWaterEntry(int x) {
        int day = GetLargestDay() + 1;
        NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO " + table_name + " (day, water_per_day) VALUES(@d, @x);", connection);
        cmd.Parameters.AddWithValue("d", day);
        cmd.Parameters.AddWithValue("x", x);
        cmd.ExecuteNonQuery();
    }

    public void UpdateByDay(int day, int amount) {
        NpgsqlCommand cmd = new NpgsqlCommand("UPDATE " + table_name + " SET water_per_day=@a WHERE day=@d;", connection);
        cmd.Parameters.AddWithValue("d", day);
        cmd.Parameters.AddWithValue("a", amount);
        cmd.ExecuteNonQuery();
    }

    public List<HabitTableObject> GetEntries() {
        List<HabitTableObject> ans = (List<HabitTableObject>)connection.Query<HabitTableObject>("SELECT * FROM " + table_name + " ORDER BY day;");
        return ans;
    }

    private void UpdateDaysAboveNumber(int n) {
        NpgsqlCommand cmd = new NpgsqlCommand("UPDATE " + table_name + " SET day=day - 1 WHERE day > @d;", connection);
        cmd.Parameters.AddWithValue("d", n);
        cmd.ExecuteNonQuery();
    }

    public void DeleteDay(int day) {
        NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM " + table_name + " WHERE day=@d;", connection);
        cmd.Parameters.AddWithValue("d", day);
        cmd.ExecuteNonQuery();
        if(GetLargestDay() != 0) UpdateDaysAboveNumber(day);
    }

    public int GetLargestDay() {
        List<HabitTableObject> list = (List<HabitTableObject>)connection.Query<HabitTableObject>("SELECT * FROM " + table_name + " ORDER BY day;");
        int day = list.Count() != 0 ? list.Last().Day : 0;
        return day;
    }

    private void CreateHabitDatabase() {
        NpgsqlCommand cmd = new NpgsqlCommand("CREATE DATABASE " + database_name + ";", connection);
        cmd.ExecuteNonQuery();
    }

    private void CreateHabitTable() {
        NpgsqlCommand cmd = new NpgsqlCommand("CREATE TABLE " + table_name + "(day INTEGER PRIMARY KEY, water_per_day INTEGER)" + ";", connection);
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