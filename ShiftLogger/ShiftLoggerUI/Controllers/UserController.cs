using RestSharp;

namespace ShiftLoggerUI.Controllers;

public class UserController
{
    private static readonly string apiString = "http://localhost:5219/shiftlogger/user";
    private RestClient client;

    public UserController()
    {
        client = new RestClient(apiString);
    }

    public bool CreateUser(string username, string password)
    {
        RestRequest request = new RestRequest($"/createuser/{username}/{password}", Method.Post);
        var response = client.ExecuteAsync(request);
        return response.Result.StatusCode == System.Net.HttpStatusCode.OK;
    }

    public int Login(string username, string password)
    {
        RestRequest request = new RestRequest($"/login/{username}/{password}", Method.Put);
        var response = client.ExecuteAsync(request);
        if(response.Result.StatusCode != System.Net.HttpStatusCode.OK) return -1;
        string? rawResult = response.Result.Content;
        Console.WriteLine(rawResult);
        return GetKey(rawResult);
    }

    public bool Logout(string username, int key)
    {
        RestRequest request = new RestRequest($"/logout/{username}/{key}", Method.Put);
        var response = client.ExecuteAsync(request);
        return response.Result.StatusCode == System.Net.HttpStatusCode.OK;
    }

    public bool IsCorrectLogin(string username, int key)
    {
        RestRequest request = new RestRequest($"/loggedin/{username}/{key}", Method.Get);
        var response = client.ExecuteAsync(request);
        return response.Result.StatusCode == System.Net.HttpStatusCode.OK;
    }

    private int GetKey(string? str)
    {
        if(str == null) return -1;
        char[] arr = str.ToCharArray();
        int raise = 0;
        int ans = 0;
        for(int i = arr.Length - 1; i > arr.Length - 6; i--)
        {
            if(arr[i] >= '0' && arr[i] <= '9') ans += (arr[i] - '0') * (int)Math.Pow(10, raise++);
        }
        return ans == 0 ? -1 : ans;
    }

}