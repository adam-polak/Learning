namespace ShiftLoggerAPI.DataAccess;

public class Shift
{
    public required string Username { get; set; }
    public int Id { get; set; }
    public required string Start_Time { get; set;}
    public required string End_Time { get; set; }
}