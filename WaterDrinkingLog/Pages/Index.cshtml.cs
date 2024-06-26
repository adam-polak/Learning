using Dapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using WaterLogger.Models;

namespace WaterLogger.Pages;

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
        ViewData["Total"] = Records.AsEnumerable().Sum(x => x.Quantity);
    }

    private List<DrinkingWaterModel> GetAllRecords()
    {
        return new List<DrinkingWaterModel>();
    }
}

