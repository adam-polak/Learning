using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using Dapper;
using SuperMovies.Models;

namespace SuperMovies.Pages;

public class DeleteModel : PageModel
{

    private IConfiguration _configuration;

    [BindProperty]
    public Movie DeleteMovie { get; set; }

    public DeleteModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult OnGet(int id)
    {
        using(NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
        {
            connection.Open();
            DeleteMovie = GetById(connection, id);
            connection.Close();
        }
        return Page();
    }

    public IActionResult OnPost()
    {
        using(NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
        {
            connection.Open();
            DeleteById(connection, DeleteMovie.Id);
            connection.Close();
            return RedirectToPage("./Index");
        }
    }

    private void DeleteById(NpgsqlConnection connection, int id)
    {
        NpgsqlCommand cmd = new NpgsqlCommand($"DELETE FROM movie_table WHERE id={id};", connection);
        cmd.ExecuteNonQuery();
    }

    private Movie GetById(NpgsqlConnection connection, int id)
    {
        List<Movie> movies = (List<Movie>)connection.Query<Movie>($"SELECT * FROM movie_table WHERE id={id};");
        return movies.ElementAt(0);
    }
}