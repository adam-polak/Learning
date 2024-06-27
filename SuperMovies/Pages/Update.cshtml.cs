using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using SuperMovies.Models;

namespace SuperMovies.Pages;

public class UpdateModel : PageModel
{

    private IConfiguration _configuration;

    [BindProperty]
    public Movie UpdateMovie { get; set; }

    public UpdateModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult OnGet(int id)
    {
        using(NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
        {
            connection.Open();
            UpdateMovie = GetById(connection, id);
            connection.Close();
        }
        return Page();
    }

    public IActionResult OnPost()
    { 
        if(!ModelState.IsValid) return Page();

        using(NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
        {
            connection.Open();
            UpdateById(connection, UpdateMovie.Id);
            connection.Close();
            return RedirectToPage("./Index");
        }
    }

    private void UpdateById(NpgsqlConnection connection, int id)
    {
        string sqlCommand = $"UPDATE movie_table SET title='{UpdateMovie.Title}', description='{UpdateMovie.Description}', length_minutes={UpdateMovie.Length_Minutes}"
                            + $" WHERE id={id};";
        NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, connection);
        cmd.ExecuteNonQuery();
    }

    private Movie GetById(NpgsqlConnection connection, int id)
    {
        List<Movie> movies = (List<Movie>)connection.Query<Movie>($"SELECT * FROM movie_table WHERE id={id};");
        return movies.ElementAt(0);
    }
}