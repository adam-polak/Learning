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
    public string? ErrorMessage;
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

    public void OnGet(string? errorMessage)
    {
        ErrorMessage = errorMessage;
        if(ErrorMessage != null) ErrorMessage = SetErrorMessage();
        if(!string.IsNullOrEmpty(SearchFor))
        {
            Movies = new List<Movie>();
            foreach(Movie movie in movieList)
            {
                if(movie.Title.Contains(SearchFor)) Movies.Add(movie);
            }
        } else Movies = movieList;
    }

    private Movie? GetMovieById(int id)
    {
        using(NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
        {
            connection.Open();
            List<Movie> movies = (List<Movie>)connection.Query<Movie>($"SELECT * FROM movie_table WHERE id={id};");
            connection.Close();
            if(movies.Count == 0) return null;
            return movies.ElementAt(0);
        }
    }

    private string? SetErrorMessage()
    {
        if(ErrorMessage == null) return null;
        bool firstPart = true;
        string error = "";
        string number = "";
        char[] arr = ErrorMessage.ToCharArray();
        for(int i = 0; i < arr.Length; i++)
        {
            if(firstPart && arr[i] != '-') error += arr[i];
            else {
                firstPart = false;
                if(arr[i] != '-') number += arr[i];
            }
        }
        int id;
        if(Int32.TryParse(number, out id))
        {
            Movie? movie = GetMovieById(id);
            if(movie == null) return null;
            switch(error)
            {
                case "exists":
                    return $"Couldn't add \"{movie.Title}\" because it already exists";
                default:
                    return null;
            }
        } else return null;
    }

}
