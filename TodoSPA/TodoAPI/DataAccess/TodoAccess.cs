using Dapper;
using Npgsql;
using TodoAPI.DataAccess.Models;

namespace TodoAPI.DataAccess;

public class TodoAccess
{
    public void AddTodo(Todo todo)
    {
        using(NpgsqlConnection connection = new NpgsqlConnection(DataAccess.ConnectionString))
        {
            connection.Open();
            if(ContainsTodo(connection, todo.Description)) throw new Exception("Todo already exists");
            todo.Id = SetId(connection);
            string sqlCommand = "INSERT INTO todo_table (id, description, completed) VALUES"
                                + $" ({todo.Id}, '{todo.Description}', {todo.Completed});";
            NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }

    public void UpdateTodo(Todo todo)
    {
        using(NpgsqlConnection connection = new NpgsqlConnection(DataAccess.ConnectionString))
        {
            connection.Open();
            if(!ContainsTodo(connection, todo.Id)) throw new Exception("Couldn't find todo");
            string sqlCommand = "UPDATE todo_table SET"
                                + $" description='{todo.Description}', completed={todo.Completed}"
                                + $" WHERE id={todo.Id};";
            NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }

    public void DeleteTodo(int id)
    {
        using(NpgsqlConnection connection = new NpgsqlConnection(DataAccess.ConnectionString))
        {
           connection.Open();
            if(!ContainsTodo(connection, id)) throw new Exception("Couldn't find todo");
            NpgsqlCommand cmd = new NpgsqlCommand($"DELETE FROM todo_table WHERE id={id};", connection);
            cmd.ExecuteNonQuery();
           connection.Close(); 
        }
    }

    private int SetId(NpgsqlConnection connection)
    {
        List<Todo> todos = (List<Todo>)connection.Query<Todo>("SELECT * FROM todo_table;");
        int largest = 0;
        foreach(Todo todo in todos) largest = Math.Max(largest, todo.Id);
        return largest + 1;
    }

    private bool ContainsTodo(NpgsqlConnection connection, int id)
    {
        List<Todo> todos = (List<Todo>)connection.Query<Todo>($"SELECT * FROM todo_table WHERE id={id};");
        return todos.Count > 0;
    }

    private bool ContainsTodo(NpgsqlConnection connection, string description)
    {
        List<Todo> todos = (List<Todo>)connection.Query<Todo>($"SELECT * FROM todo_table WHERE description='{description}';");
        return todos.Count > 0;
    }

}