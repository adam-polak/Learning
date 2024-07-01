using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;
using Newtonsoft.Json;
using TodoAPP.Models;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace TodoAPP.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private IConfiguration _configuration;
    private RestClient client;
    public List<Todo> Todos;

    public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        client = new RestClient(_configuration.GetConnectionString("TodoAPI") ?? "");
        Todos = GetTodos();
    }

    public void OnGet()
    {

    }

    public IActionResult OnPostCompleted(int id, bool completed, string description)
    {
        UpdateTodo(id, !completed, description);
        return Page();
    }

    public IActionResult OnPostDescription(int id, bool completed, string description)
    {
        UpdateTodo(id, completed, description);
        return Page();
    }

    private void UpdateTodo(int id, bool completed, string description)
    {
        using(HttpClient httpClient = new HttpClient())
        {
            httpClient.BaseAddress = new Uri(_configuration.GetConnectionString("TodoAPI") ?? "");
            var response = httpClient.PutAsJsonAsync($"update/{id}/{completed}", description);
            if(response.Result.StatusCode == HttpStatusCode.OK) Todos = GetTodos();
        }
    }

    public IActionResult OnPostDelete(int id)
    {
        RestRequest request = new RestRequest($"delete/{id}", Method.Delete);
        var response = client.ExecuteAsync(request);
        if(response.Result.StatusCode == HttpStatusCode.OK) Todos = GetTodos();
        return Page();
    }

    public IActionResult OnPostAdd(string description)
    {
        using(HttpClient httpClient = new HttpClient())
        {
            httpClient.BaseAddress = new Uri(_configuration.GetConnectionString("TodoAPI") ?? "");
            var response = httpClient.PostAsJsonAsync("add/false", description);
            if(response.Result.StatusCode == HttpStatusCode.OK) Todos = GetTodos();
            else Console.WriteLine(response.Result.StatusCode);
        }
        return Page();
    }

    private List<Todo> GetTodos()
    {
        RestRequest request = new RestRequest("todos", Method.Get);
        var response = client.ExecuteAsync(request);
        if(response.Result.StatusCode != System.Net.HttpStatusCode.OK) return new List<Todo>();
        string? rawResult = response.Result.Content;
        List<Todo> Todos = new List<Todo>();
        if(rawResult == null || rawResult.Length  <= 2) return Todos;
        Todo? addTodo;
        while(rawResult.Length > 0) 
        {
            string[] arr = GetJsonObject(rawResult);
            string jsonObject = arr[0];
            rawResult = arr[1];
            addTodo = JsonConvert.DeserializeObject<Todo>(jsonObject);
            if(addTodo != null) 
            {
                Todos.Add(addTodo);
            }
        }
        Todos.Sort(delegate(Todo x, Todo y)
        {
            if(x.Id == y.Id) return 0;
            else if(x.Id < y.Id) return -1;
            else return 1;
        });
        return Todos;
    }

    private string[] GetJsonObject(string str)
    {
        string ans = "";
        char[] arr = str.ToCharArray();
        for(int i = 0; i < arr.Length; i++)
        {
            if(arr[i] == '\\') continue;
            ans += arr[i];
            if(arr[i] == '}') 
            {
                str = MakeSubstring(arr, i + 1);
                break;
            }
        }
        if(ans.ElementAt(0) == '"') ans = ans.Substring(1, ans.Length - 1);
        return [ans, str];
    }

    private string MakeSubstring(char[] arr, int start)
    {
        string ans = "";
        while(start < arr.Length) ans += arr[start++];
        if(ans.Length == 1) ans = "";
        return ans;
    }
}
