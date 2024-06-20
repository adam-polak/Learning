using Npgsql;
using Dapper;
using ShiftLoggerAPI.DataAccess.Models;

namespace ShiftLoggerAPI.DataAccess;

public class ShiftTableAccess
{
    
    NpgsqlConnection connection;
    UserTableAccess userTable;

    public ShiftTableAccess()
    {
        userTable = new UserTableAccess();
        connection = new NpgsqlConnection(DataAccess.ConnectionString);
        connection.Open();
    }

    public string StartShift(string username, int key)
    {
        userTable.ValidateUsernameAndSessionKey(username, key);
        if(!IsLastEnded(username)) throw new Exception("Last shift hasn't ended yet");
        Shift? last = GetLast(username);
        int id = last == null ? 1 : last.Id + 1;
        Shift cur = new Shift() { Username = username, Id = id, Start_Time = DateTime.Now.ToString(), End_Time = "ongoing" };
        NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO shift_table (username, id, start_time, end_time) VALUES (@u, @i, @s, @e);", connection);
        cmd.Parameters.AddWithValue("u", username);
        cmd.Parameters.AddWithValue("i", cur.Id);
        cmd.Parameters.AddWithValue("s", cur.Start_Time);
        cmd.Parameters.AddWithValue("e", cur.End_Time);
        cmd.ExecuteNonQuery();
        return cur.Start_Time;
    }

    public string EndShift(string username, int key)
    {
        userTable.ValidateUsernameAndSessionKey(username, key);
        Shift? last = GetLast(username);
        if(last == null) throw new Exception("Shift hasn't been started yet");
        if(!"ongoing".Equals(last.End_Time)) throw new Exception("Shift has already ended");
        last.End_Time = DateTime.Now.ToString();
        NpgsqlCommand cmd = new NpgsqlCommand("UPDATE shift_table SET end_time=@e WHERE username=@u AND start_time=@s;", connection);
        cmd.Parameters.AddWithValue("e", last.End_Time);
        cmd.Parameters.AddWithValue("u", username);
        cmd.Parameters.AddWithValue("s", last.Start_Time);
        cmd.ExecuteNonQuery();
        return last.End_Time;
    }

    public List<Shift> ViewShifts(string username, int key)
    {
        userTable.ValidateUsernameAndSessionKey(username, key);
        return (List<Shift>)connection.Query<Shift>($"SELECT * FROM shift_table WHERE username='{username}';");
    }

    private bool IsLastEnded(string username)
    {
        List<Shift> shifts = (List<Shift>)connection.Query<Shift>($"SELECT * FROM shift_table WHERE username='{username}';");
        Shift? lastShift = shifts.LastOrDefault();
        return lastShift == null || !"ongoing".Equals(lastShift.End_Time);
    }

    private Shift? GetLast(string username)
    {
        List<Shift> shifts = (List<Shift>)connection.Query<Shift>($"SELECT * FROM shift_table WHERE username='{username}';");
        return shifts.Count() == 0 ? null : shifts.Last();
    }

}