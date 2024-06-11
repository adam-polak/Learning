using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary;

class FlashCard {
    public int Id { get; set; }
    public string? Type { get; set; }
    public string? Front { get; set; }
    public string? Back { get; set; }
}