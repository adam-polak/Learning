using Microsoft.AspNetCore.Mvc;
using TodoAPI.DataAccess;
using TodoAPI.DataAccess.Models;
using System.Text;
using Newtonsoft.Json;

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

    [HttpGet("todos")]
    public IActionResult GetTodos()
    {
        StringBuilder sb = new StringBuilder();
        List<Todo> todos = todoAccess.GetTodos();
        if(todos.Count > 0)
        {
            JsonWriter writer = new JsonTextWriter(new StringWriter(sb));
            foreach(Todo todo in todos) 
            {
                writer.WriteStartObject();
                writer.WritePropertyName("Id");
                writer.WriteValue(todo.Id);
                writer.WritePropertyName("Description");
                writer.WriteValue(todo.Description);
                writer.WritePropertyName("Completed");
                writer.WriteValue(todo.Completed);
                writer.WriteEndObject();
            }
        }
        return Ok(sb.ToString());
    }

    [HttpPost("add/{completed}")]
    public IActionResult AddTodo(bool completed, [FromBody] string description)
    {
        Todo todo = new Todo() { Description = description, Completed = completed };
        try
        {
            todoAccess.AddTodo(todo);
            return Ok();
        } catch(Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("update/{id}/{completed}")]
    public IActionResult UpdateTodo(int id, bool completed, [FromBody] string description)
    {
        Todo todo = new Todo() { Id = id, Description = description, Completed = completed };
        try
        {
            todoAccess.UpdateTodo(todo);
            return Ok();
        } catch(Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("delete/{id}")]
    public IActionResult DeleteTodo(int id)
    {
        try
        {
            todoAccess.DeleteTodo(id);
            return Ok();
        } catch(Exception e) {
            return BadRequest(e.Message);
        }
    }
}