using Microsoft.AspNetCore.Mvc;
using TodoAPI.DataAccess;

namespace TodoAPI.Controllers;

[ApiController]
[Route("todoapi")]
public class TodoController : ControllerBase
{
    TodoAccess todoAccess;

    public TodoController()
    {
        todoAccess = new TodoAccess();
    }

    [HttpPost("add/{completed}")]
    public IActionResult AddTodo(bool completed, [FromBody] string description)
    {
        return Ok();
    }

    [HttpPut("update/{id}/{completed}")]
    public IActionResult UpdateTodo(int id, bool completed, [FromBody] string description)
    {
        return Ok();
    }

    [HttpDelete("delete/{id}")]
    public IActionResult DeleteTodo(int id)
    {
        return Ok();
    }
}