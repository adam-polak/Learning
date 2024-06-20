using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShiftLoggerAPI.DataAccess;

namespace ShiftLoggerAPI.Controller;

[ApiController]
[Route("shiftlogger/shift")]
public class ShiftController : ControllerBase
{

    ShiftTableAccess shiftTable;

    public ShiftController()
    {
        shiftTable = new ShiftTableAccess();
    }

    [HttpPost("start_shift/{username}/{key}")]
    public IActionResult StartShift(string username, int key)
    {
        try {
            string dateTime = shiftTable.StartShift(username, key);
            return Ok($"Started shift for {username} at {dateTime}");
        } catch(Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("end_shift/{username}/{key}")]
    public IActionResult EndShift(string username, int key)
    {
        try {
            string dateTime = shiftTable.EndShift(username, key);
            return Ok($"Ended shift for {username} at {dateTime}");
        } catch(Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("view_shifts/{username}/{key}")]
    public IActionResult ViewShifts(string username, int key)
    {
        try {
            List<Shift> shifts = shiftTable.ViewShifts(username, key);
            StringBuilder sb = new StringBuilder();
            JsonWriter writer = new JsonTextWriter(new StringWriter(sb));
            writer.WriteStartObject();
            foreach(Shift shift in shifts)
            {
                writer.WritePropertyName($"{shift.Id}");
                writer.WriteStartArray();
                writer.WriteValue(shift.Start_Time);
                writer.WriteValue(shift.End_Time);
                writer.WriteEndArray();
            }
            writer.WriteEndObject();
            return Ok(sb.ToString());
        } catch(Exception e) {
            return BadRequest(e.Message);
        }
    }

}