using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;
using Newtonsoft.Json;
using TodoAPP.Models;

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
    }

    public void OnGet()
    {
        Todos = GetTodos();
    }

    public void OnPost(int id)
    {
        
    }

    private List<Todo> GetTodos()
    {
        RestRequest request = new RestRequest($"/todos", Method.Get);
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
