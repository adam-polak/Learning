using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using WaterLogger.Models;

namespace WaterLogger.Pages
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
            return new DrinkingWaterModel();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            return RedirectToPage("./Index");
        }
    }
}