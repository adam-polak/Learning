using System.ComponentModel.DataAnnotations;

namespace SuperMovies.Models;

public class Movie
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Movie must have a title.")]
    public required string Title { get; set; }
    [Required(ErrorMessage = "Movie must have a description.")]
    public required string Description { get; set; }
    [Range(0, Int32.MaxValue, ErrorMessage = "Value must be positive.")]
    public int Length_Minutes { get; set; }
}