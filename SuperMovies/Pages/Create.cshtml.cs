using System.ComponentModel.Design;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using SuperMovies.Models;

namespace SuperMovies.Pages;

public class CreateModel : PageModel
{
    IConfiguration _configuration;

    [BindProperty]
    public Movie AddMovie { get; set; }

    public CreateModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    public IActionResult OnPost()
    {
        if(!ModelState.IsValid) return Page();

        using(NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
        {
            connection.Open();
            AddMovie.Id = SetId(connection);
            string sqlCommand = "INSERT INTO movie_table (id, title, description, length_minutes)"
                                + $" VALUES ({AddMovie.Id}, '{AddMovie.Title}', '{AddMovie.Description}', {AddMovie.Length_Minutes})";
            NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        return RedirectToPage("./Index");
    }

    private int SetId(NpgsqlConnection connection)
    {
        int largest = 0;
        List<Movie> movies = (List<Movie>)connection.Query<Movie>("SELECT * FROM movie_table;");
        foreach(Movie movie in movies) largest = Math.Max(largest, movie.Id);
        return largest + 1;
    }
}