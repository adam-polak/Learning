using Microsoft.AspNetCore.Mvc;

namespace ShiftLoggerAPI.Controller;

[ApiController]
[Route("shiftlogger/shift")]
public class ShiftController : ControllerBase
{

    [HttpPost("startshift/{username}")]
    public IActionResult StartShift(string username)
    {
        return Ok($"Started shift for {username}");
    }

    [HttpPut("endshift/{username}")]
    public IActionResult EndShift(string username)
    {
        return Ok($"Ended shift for {username}");
    }

}