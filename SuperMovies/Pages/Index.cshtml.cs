using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using SuperMovies.Models;

namespace SuperMovies.Pages;

public class IndexModel : PageModel
{
    private IConfiguration _configuration;
    private readonly ILogger<IndexModel> _logger;
    [BindProperty(SupportsGet = true)]
    public string? SearchFor { get; set; }
    public List<Movie> Movies;
    private List<Movie> movieList;

    public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        Movies = [];
        movieList = GetMovies();
    }

    private List<Movie> GetMovies()
    {
        using(NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
        {
            connection.Open();
            List<Movie> list = (List<Movie>)connection.Query<Movie>("SELECT * FROM movie_table;");
            connection.Close();
            return list;
        }
    }

    public void OnGet()
    {
        if(!string.IsNullOrEmpty(SearchFor))
        {
            Movies = new List<Movie>();
            foreach(Movie movie in movieList)
            {
                if(movie.Title.Contains(SearchFor)) Movies.Add(movie);
            }
        } else Movies = movieList;
    }
}
