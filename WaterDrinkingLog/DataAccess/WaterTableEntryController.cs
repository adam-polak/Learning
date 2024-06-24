

using Dapper;
using Npgsql;
using WaterDrinkingLog.DataAccess.Models;

namespace WaterDrinkingLog.DataAccess.Controllers;

public class WaterTableEntryController
{

    private NpgsqlConnection connection;

    public WaterTableEntryController(IConfiguration configuration)
    {
        connection = new NpgsqlConnection(configuration.GetConnectionString("Default"));
    }

    public void AddGlassOfWater()
    {
        connection.Open();
        string today = DateTime.Now.ToShortDateString();
        WaterTableEntry? entry = connection.Query<WaterTableEntry>($"SELECT * FROM water_table WHERE date='{today}';").First();
        if(entry == null) InsertDay(today);
        else UpdateDay(today);
        connection.Close();
    }

    public bool RemoveGlassOfWater()
    {
        return false;
    }

    private void UpdateDay(string date)
    {
        NpgsqlCommand cmd = new NpgsqlCommand($"UPDATE water_table SET water_drank=water_drank + 1 WHERE date='{date}';", connection);
        cmd.ExecuteNonQuery();
    }

    private void InsertDay(string date)
    {
        NpgsqlCommand cmd = new NpgsqlCommand($"INSERT INTO water_table (date, water_drank) VALUES ('{date}', 1);", connection);
        cmd.ExecuteNonQuery();
    }

}