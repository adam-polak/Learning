using Dapper;
using Npgsql;
using WaterDrinkingLog.DataAccess.Models;

namespace WaterDrinkingLog.DataAccess.Controllers;

public class WaterController
{

    private NpgsqlConnection connection;

    public WaterController(IConfiguration configuration)
    {
        connection = new NpgsqlConnection(configuration.GetConnectionString("Default"));
    }

    public void AddGlassOfWater()
    {
        connection.Open();
        string today = DateTime.Now.ToShortDateString();
        WaterTableEntry? entry = GetDay(today);
        if(entry == null) InsertDay(today, 1);
        else UpdateDay(today, entry.Water_Drank + 1);
        connection.Close();
    }

    public bool RemoveGlassOfWater()
    {
        connection.Open();
        string today = DateTime.Now.ToShortDateString();
        WaterTableEntry? entry = GetDay(today);
        bool remove = entry != null && entry.Water_Drank > 0;
        if(remove && entry != null) UpdateDay(today, entry.Water_Drank - 1);
        connection.Close();
        return remove;
    }

    public List<WaterTableEntry> GetWaterTableEntries()
    {
        connection.Open();
        List<WaterTableEntry> list = (List<WaterTableEntry>)connection.Query<WaterTableEntry>("SELECT * FROM water_table;");
        connection.Close();
        return list;
    }

    private WaterTableEntry? GetDay(string date)
    {
        return connection.Query<WaterTableEntry>($"SELECT * FROM water_table WHERE date='{date}';").First();
    }

    private void UpdateDay(string date, int amount)
    {
        NpgsqlCommand cmd;
        if(amount == 0) cmd = new NpgsqlCommand($"DELETE FROM water_table WHERE date='{date}';");
        else cmd = new NpgsqlCommand($"UPDATE water_table SET water_drank={amount} WHERE date='{date}';", connection);
        cmd.ExecuteNonQuery();
    }

    private void InsertDay(string date, int amount)
    {
        NpgsqlCommand cmd = new NpgsqlCommand($"INSERT INTO water_table (date, water_drank) VALUES ('{date}', {amount});", connection);
        cmd.ExecuteNonQuery();
    }

}