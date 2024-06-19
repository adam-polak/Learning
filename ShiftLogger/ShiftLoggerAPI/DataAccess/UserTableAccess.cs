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
        ValidateLogin(username, password);
        int key = GenerateSessionKey();
        NpgsqlCommand cmd = new NpgsqlCommand("UPDATE user_table SET logged_in=@k WHERE username=@u;", connection);
        cmd.Parameters.AddWithValue("k", key);
        cmd.Parameters.AddWithValue("u", username);
        cmd.ExecuteNonQuery();
        return key;
    }

    public void LogoutOfUser(string username, int session_key)
    {
        ValidateLogout(username, session_key);
        NpgsqlCommand cmd = new NpgsqlCommand("UPDATE user_table SET logged_in='false' WHERE username=@u; AND logged_in=@k;", connection);
        cmd.Parameters.AddWithValue("u", username);
        cmd.Parameters.AddWithValue("k", session_key);
        cmd.ExecuteNonQuery();
    }

    public bool ContainsUsername(string username)
    {
        List<User> users = (List<User>)connection.Query<User>($"SELECT * FROM user_table WHERE username='{username}';");
        return users.Count() == 0;
    }

    public bool CorrectSessionId(string username, int session_key)
    {
        List<User> users = (List<User>)connection.Query<User>($"SELECT * FROM user_table WHERE username='{username}' AND logged_in='{session_key}';");
        return users.Count() == 0;
    }

    private void ValidateCreateUser(string username, string password)
    {
        if(!ContainsUsername(username)) throw new Exception("Username already exists");
        if(!ValidatePassword(password)) throw new Exception("Password needs to be atleast 6 characters");
    }

    private void ValidateLogout(string username, int session_key)
    {
        if(!ContainsUsername(username)) throw new Exception("Username does not exist");
        if(!CorrectSessionId(username, session_key)) throw new Exception("Incorrect session key");
    }

    private void ValidateLogin(string username, string password)
    {
        if(!ContainsUsername(username)) throw new Exception("Username does not exist");
        if(!CorrectPassword(username, password)) throw new Exception("Password is incorrect");
        if(IsOtherUserLoggedIn(username)) throw new Exception("Other user is logged in");
    }

    private bool IsOtherUserLoggedIn(string username)
    {
        List<User> users = (List<User>)connection.Query<User>($"SELECT * FROM user_table WHERE username='{username}' AND logged_in='false';");
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