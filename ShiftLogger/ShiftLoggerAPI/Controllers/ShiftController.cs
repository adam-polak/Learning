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
            DateTime dateTime = shiftTable.StartShift();
            return Ok($"Started shift for {username} at {dateTime.ToShortDateString()}");
        } catch(Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("end_shift/{username}/{key}")]
    public IActionResult EndShift(string username, int key)
    {
        try {
            DateTime dateTime = shiftTable.EndShift();
            return Ok($"Ended shift for {username} at {dateTime.ToShortTimeString()}");
        } catch(Exception e) {
            return BadRequest(e.Message);
        }
    }

}