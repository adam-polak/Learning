using Microsoft.AspNetCore.Mvc;
using ShiftLoggerAPI.DataAccess;

namespace ShiftLoggerAPI.Controller;

[ApiController]
[Route("shiftlogger/user")]
public class UserController : ControllerBase
{

    UserTableAccess userTable;

    public UserController()
    {
        userTable = new UserTableAccess();
    }

    [HttpPost("createuser/{username}/{password}")]
    public IActionResult CreateUser(string username, string password)
    {
        return Ok($"Created user {username} with password with length of {password.Length}");
    }

    [HttpPut("login/{username}/{password}")]
    public IActionResult LoginToUser(string username, string password)
    {
        try {
            int key = userTable.LoginToUser(username, password);
            return Ok($"Logged in to user {username} the key is {key}");
        } catch(Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("logout/{username}/{key}")]
    public IActionResult LogoutOfUser(string username, int key)
    {
        try {
            userTable.LogoutOfUser(username, key);
            return Ok($"Logged out of {username}");
        } catch(Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("loggedin/{username}/{session_key}")]
    public IActionResult IsLoggedIn(string username, string session_key)
    {
        return Ok($"Logged in as {username} with {session_key}");
    }

}