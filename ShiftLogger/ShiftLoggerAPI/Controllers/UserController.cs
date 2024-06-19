using Microsoft.AspNetCore.Mvc;

namespace ShiftLoggerAPI.Controller;

[ApiController]
[Route("shiftlogger/user")]
public class UserController : ControllerBase
{

    [HttpPost("createuser/{username}/{password}")]
    public IActionResult CreateUser(string username, string password)
    {
        return Ok($"Created user {username} with password with length of {password.Length}");
    }

    [HttpPost("login/{username}/{password}")]
    public IActionResult LoginToUser(string username, string password)
    {
        return Ok($"Logged in to user {username}");
    }

    [HttpGet("loggedin/{username}/{session_key}")]
    public IActionResult IsLoggedIn(string username, string session_key)
    {
        return Ok($"Logged in as {username} with {session_key}");
    }

}