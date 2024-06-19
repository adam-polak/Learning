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
    
    public bool ContainsUsername(string username)
    {
        return false;
    }

}