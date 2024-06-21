using Newtonsoft.Json;
using RestSharp;
using ShiftLoggerUI.Models;

namespace ShiftLoggerUI.Controllers;

public class ShiftController
{
    private static readonly string apiString = "http://localhost:5219/shiftlogger/shift";
    private RestClient client;

    public ShiftController()
    {
        client = new RestClient(apiString);
    }

    public string StartShift(string username, int key)
    {
        RestRequest request = new RestRequest($"/start_shift/{username}/{key}", Method.Post);
        var response = client.ExecuteAsync(request);
        return response.Result.Content ?? "";
    }

    public string EndShift(string username, int key)
    {
        RestRequest request = new RestRequest($"/end_shift/{username}/{key}", Method.Put);
        var response = client.ExecuteAsync(request);
        return response.Result.Content ?? "";
    }

    public List<Shift> ViewShifts(string username, int key)
    {
        RestRequest request = new RestRequest($"/view_shifts/{username}/{key}", Method.Get);
        var response = client.ExecuteAsync(request);
        if(response.Result.StatusCode != System.Net.HttpStatusCode.OK) return new List<Shift>();
        string? rawResult = response.Result.Content;
        List<Shift> shifts = new List<Shift>();
        if(rawResult == null || rawResult.Length  <= 2) return shifts;
        Shift? addShift;
        while(rawResult.Length > 0) 
        {
            string[] arr = GetJsonObject(rawResult);
            string jsonObject = arr[0];
            rawResult = arr[1];
            Console.WriteLine(jsonObject);
            addShift = JsonConvert.DeserializeObject<Shift>(jsonObject);
            if(addShift != null) shifts.Add(addShift);
        }
        return shifts;
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
                str = Substring(arr, i + 1);
                break;
            }
        }
        if(ans.ElementAt(0) == '"') ans = ans.Substring(1, ans.Length - 1);
        return [ans, str];
    }

    private string Substring(char[] arr, int start)
    {
        string ans = "";
        while(start < arr.Length) ans += arr[start++];
        if(ans.Length == 1) ans = "";
        return ans;
    }


}