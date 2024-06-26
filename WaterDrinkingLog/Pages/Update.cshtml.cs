using System.Globalization;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using WaterDrinkingLog.Models;

namespace WaterDrinkingLog.Pages
{
    public class UpdateModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public DrinkingWaterModel DrinkingWater { get; set; }

        public UpdateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet(int id)
        {
            DrinkingWater = GetById(id);

            return Page();
        }

        private DrinkingWaterModel GetById(int id)
        {
            using(NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                DrinkingWaterModel? ans = connection.Query<DrinkingWaterModel>($"SELECT * FROM drinking_water WHERE id={id};").FirstOrDefault();
                connection.Close();
                return ans ?? new DrinkingWaterModel();
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            
            using(NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                string shortDate = DrinkingWater.Date.ToShortDateString();
                string sqlCommand = "UPDATE drinking_water"
                                    + $" SET date='{shortDate}', quantity={DrinkingWater.Quantity}"
                                    + $" WHERE id={DrinkingWater.Id};";
                NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }

            return RedirectToPage("./Index");
        }
    }
}