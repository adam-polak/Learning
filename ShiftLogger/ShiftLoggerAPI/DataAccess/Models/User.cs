namespace ShiftLoggerAPI.DataAccess;

public class User
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string LoggedIn { get; set; }
}