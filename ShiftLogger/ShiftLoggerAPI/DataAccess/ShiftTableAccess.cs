using Npgsql;
using Dapper;

namespace ShiftLoggerAPI.DataAccess;

public class ShiftTableAccess
{
    
    NpgsqlConnection connection;

    public ShiftTableAccess()
    {
        connection = new NpgsqlConnection(DataAccess.ConnectionString);
        connection.Open();
    }

    public DateTime StartShift()
    {
        return DateTime.Now;
    }

    public DateTime EndShift()
    {
        return DateTime.Now;
    }

}