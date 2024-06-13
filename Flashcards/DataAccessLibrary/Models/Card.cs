using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary;

public class Card
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Front { get; set; }
    public required string Back { get; set; }
}