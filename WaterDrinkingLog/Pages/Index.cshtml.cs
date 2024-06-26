using Dapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using WaterDrinkingLog.Models;

namespace WaterDrinkingLog.Pages;

public class IndexModel : PageModel
{
    private readonly IConfiguration _configuration;

    public List<DrinkingWaterModel> Records { get; set; }

    public IndexModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void OnGet()
    {
        Records = GetAllRecords();
    }

    private List<DrinkingWaterModel> GetAllRecords()
    {
        using(NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
        {
            connection.Open();
            List<DrinkingWaterModel> list = (List<DrinkingWaterModel>)connection.Query<DrinkingWaterModel>("SELECT * FROM drinking_water;");
            connection.Close();
            return list;
        }
    }
}

