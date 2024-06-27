using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuperMovies.Models;

namespace SuperMovies.Pages;

public class DeleteModel : PageModel
{

    ILogger _logger;
    IConfiguration _configuration;

    public DeleteModel(ILogger<IndexModel> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    public IActionResult OnPost()
    {
        if(!ModelState.IsValid) return Page();

        

        return RedirectToPage("./Index");
    }
}