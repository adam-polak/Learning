using Npgsql;
using Dapper;
using ShiftLoggerAPI.DataAccess.Models;

namespace ShiftLoggerAPI.DataAccess;

public class UserTableAccess
{

    NpgsqlConnection connection;

    public UserTableAccess()
    {
        connection = new NpgsqlConnection(DataAccess.ConnectionString);
    }

    private void Open()
    {
        if(connection.State != System.Data.ConnectionState.Open) connection.Open();
    }

    public void CreateUser(string username, string password)
    {
        ValidateCreateUser(username, password);
        Open();
        NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO user_table (username, password, logged_in) VALUES (@u, @p, 'false');", connection);
        cmd.Parameters.AddWithValue("u", username);
        cmd.Parameters.AddWithValue("p", password);
        cmd.ExecuteNonQuery();
        connection.Close();
    }

    public int LoginToUser(string username, string password)
    {
        ValidateLogin(username, password);
        int key = GenerateSessionKey();
        Open();
        NpgsqlCommand cmd = new NpgsqlCommand("UPDATE user_table SET logged_in=@k WHERE username=@u;", connection);
        cmd.Parameters.AddWithValue("k", key);
        cmd.Parameters.AddWithValue("u", username);
        cmd.ExecuteNonQuery();
        connection.Close();
        return key;
    }

    public void LogoutOfUser(string username, int session_key)
    {
        ValidateUsernameAndSessionKey(username, session_key);
        Open();
        NpgsqlCommand cmd = new NpgsqlCommand("UPDATE user_table SET logged_in='false' WHERE username=@u AND logged_in=@k;", connection);
        cmd.Parameters.AddWithValue("u", username);
        cmd.Parameters.AddWithValue("k", "" + session_key);
        cmd.ExecuteNonQuery();
        connection.Close();
    }

    public bool ContainsUsername(string username)
    {
        Open();
        List<User> users = (List<User>)connection.Query<User>($"SELECT * FROM user_table WHERE username='{username}';");
        connection.Close();
        return users.Count() != 0;
    }

    public void ValidateUsernameAndSessionKey(string username, int session_key)
    {
        Open();
        if(!ContainsUsername(username)) throw new Exception("Username does not exist");
        if(!CorrectSessionId(username, session_key)) throw new Exception("Incorrect session key");
        connection.Close();
    }

    private bool CorrectSessionId(string username, int session_key)
    {
        List<User> users = (List<User>)connection.Query<User>($"SELECT * FROM user_table WHERE username='{username}' AND logged_in='{session_key}';");
        return users.Count() != 0;
    }

    private void ValidateCreateUser(string username, string password)
    {
        if(ContainsUsername(username)) throw new Exception("Username already exists");
        if(!ValidatePassword(password)) throw new Exception("Password needs to be atleast 6 characters");
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
        List<User> users = (List<User>)connection.Query<User>($"SELECT * FROM user_table WHERE username='{username}' AND password='{password}';");
        User? user = users.ElementAt(0);
        return user != null && username.Equals(user.Username) && password.Equals(user.Password);
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