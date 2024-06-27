using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SuperMovies.Pages;

public class IndexModel : PageModel
{
    private IConfiguration _configuration;
    private readonly ILogger<IndexModel> _logger;
    [BindProperty(SupportsGet = true)]
    public string SearchString { get; set; }
    public List<string> DisplayItems = [];
    private List<string> items = [ "Hi", "Bye", "Hi" ];

    public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public void OnGet()
    {
        if(!string.IsNullOrEmpty(SearchString))
        {
            DisplayItems = new List<string>();
            foreach(string item in items)
            {
                if(item.Contains(SearchString)) DisplayItems.Add(item);
            }
        } else DisplayItems = items;
    }
}
