using Npgsql;
using Dapper;

namespace ShiftLoggerAPI.DataAccess;

public class UserTableAccess
{

    NpgsqlConnection connection;

    public UserTableAccess()
    {
        connection = new NpgsqlConnection(DataAccess.ConnectionString);
    }

    public void CreateUser(string username, string password)
    {
        if(!ContainsUsername(username)) throw new Exception("Username already exists");
        if(!ValidatePassword(password)) throw new Exception("Password needs to be atleast 6 characters");
    }

    public int LoginToUser(string username, string password)
    {
        if(!ContainsUsername(username)) throw new Exception("Username does not exist");
        if(!CorrectPassword(username, password)) throw new Exception("Password is incorrect");
        return GenerateSessionKey();
    }

    public bool ContainsUsername(string username)
    {
        List<User> users = (List<User>)connection.Query<User>($"SELECT * FROM user_table WHERE username={username};");
        return users.Count() == 0;
    }

    public bool CorrectSessionId(string username, int session_key)
    {
        List<User> users = (List<User>)connection.Query<User>($"SELECT * FROM user_table WHERE username={username} AND logged_in={session_key};");
        return users.Count() == 0;
    }

    private bool CorrectPassword(string username, string password)
    {
        return false;
    }

    private bool ValidatePassword(string password)
    {
        return password.Length > 5;
    }

    private int GenerateSessionKey()
    {
        Random rnd = new Random();
        return rnd.Next(100, 10000);
    }

}