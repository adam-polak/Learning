using Npgsql;
using Dapper;

namespace ShiftLoggerAPI.DataAccess;

public class ShiftTableAccess
{
    
    NpgsqlConnection connection;

    public ShiftTableAccess()
    {
        connection = new NpgsqlConnection(DataAccess.ConnectionString);
    }

}