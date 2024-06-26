using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using WaterLogger.Models;

namespace WaterLogger.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public DrinkingWaterModel DrinkingWater { get; set; }

        public DeleteModel(IConfiguration configuration)
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

        public IActionResult OnPost(int id)
        {
            return RedirectToPage("./Index");
        }
    }
}
