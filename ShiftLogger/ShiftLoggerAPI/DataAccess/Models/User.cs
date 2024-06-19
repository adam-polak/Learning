namespace ShiftLoggerAPI.DataAccess;

class User
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string LoggedIn { get; set; }
}