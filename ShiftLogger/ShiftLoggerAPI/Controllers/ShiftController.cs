using Microsoft.AspNetCore.Mvc;
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

}