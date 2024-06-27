using Dapper;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using WaterDrinkingLog.Models;

namespace WaterDrinkingLog.Pages
{
	public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DrinkingWaterModel DrinkingWater { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            
            using(NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                if(ContainsDate(connection)) UpdateDate(connection);
                else InsertDate(connection);
                connection.Close();
            }
            
            return RedirectToPage("./Index");
        }

        private void InsertDate(NpgsqlConnection connection)
        {
            DrinkingWater.Id = GetLastId(connection) + 1;
            string shortDate = DrinkingWater.Date.ToShortDateString();
            string sqlCommand = "INSERT INTO drinking_water  (id, date, quantity)"
                                + $" VALUES ({DrinkingWater.Id}, '{shortDate}', {DrinkingWater.Quantity});";
            NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, connection);
            cmd.ExecuteNonQuery();
        }

        private void UpdateDate(NpgsqlConnection connection)
        {
            string shortDate = DrinkingWater.Date.ToShortDateString();
            string sqlCommand = "UPDATE drinking_water"
                                + $" SET quantity=quantity+{DrinkingWater.Quantity}"
                                + $" WHERE date='{shortDate}';";
            NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, connection);
            cmd.ExecuteNonQuery();
        }

        private bool ContainsDate(NpgsqlConnection connection)
        {
            List<string> list = (List<string>)connection.Query<string>("SELECT date FROM drinking_water;");
            foreach(string date in list)
            {
                if(date.Equals(DrinkingWater.Date.ToShortDateString())) return true;
            }
            return false;
        }

        private int GetLastId(NpgsqlConnection connection)
        {
            List<DrinkingWaterModel> list = (List<DrinkingWaterModel>)connection.Query<DrinkingWaterModel>("SELECT * FROM drinking_water;");
            int largestId = 0;
            foreach(DrinkingWaterModel data in list) largestId = largestId > data.Id ? largestId : data.Id;
            return largestId;
        }

    }
}
