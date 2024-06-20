namespace ShiftLoggerUI.Models;

public class Shift
{
    public int Id { get; set; }
    public required string Start_Time { get; set; }
    public required string End_Time { get; set; }
}