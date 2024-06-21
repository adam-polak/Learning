using Newtonsoft.Json;
using RestSharp;
using ShiftLoggerUI.Models;
using Spectre.Console;

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
        string result = response.Result.Content ?? "";
        Console.WriteLine(result.Substring(0, 8));
        if(result.Substring(0, 8).Equals("\"Started")) result = FixDateInResponse(result);
        return result;
    }

    public string EndShift(string username, int key)
    {
        RestRequest request = new RestRequest($"/end_shift/{username}/{key}", Method.Put);
        var response = client.ExecuteAsync(request);
        string result = response.Result.Content ?? "";
        Console.WriteLine(result.Substring(0, 6));
        if(result.Substring(0, 6).Equals("\"Ended")) result = FixDateInResponse(result);
        return result;
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
            addShift = JsonConvert.DeserializeObject<Shift>(jsonObject);
            if(addShift != null) 
            {
                FixDate(addShift);
                shifts.Add(addShift);
            }
        }
        return shifts;
    }

    private string FixDateInResponse(string response)
    {
        response = response.Substring(0, response.Length - 1);
        char[] arr = response.ToCharArray();
        int forwardSlashCount = 0;
        bool addToResponse = false;
        string date = "";
        response = "";
        for(int i = arr.Length -  1; i >= 0; i--)
        {
            if(forwardSlashCount < 2)
            {
                if(arr[i] == '/') forwardSlashCount++;
                date = arr[i] + date;
            } else if(!addToResponse) {
                if(arr[i] >= '0' && arr[i] <= '9') date = arr[i] + date;
                else addToResponse = true;
            }
            if(addToResponse) response = arr[i] + response;
        }
        date = RemoveDateError(date);
        return response + date + "\"";
    }

    private void FixDate(Shift shift)
    {
        shift.Start_Time = RemoveDateError(shift.Start_Time);
        if(!shift.End_Time.Equals("ongoing")) shift.End_Time = RemoveDateError(shift.End_Time);
    }

    private string RemoveDateError(string date)
    {
        string end = date.Substring(date.Length - 2);
        date = date.Substring(0, date.Length - 2);
        char[] arr = date.ToCharArray();
        bool add = true;
        date = "";
        for(int i = 0; i < arr.Length; i++)
        {
            if(arr[i] == ' ') add = false;
            if(add) date += arr[i];
            else if(arr[i] == ' ' || arr[i] == ':' || (arr[i] >= '0' && arr[i] <= '9')) date += arr[i];
            else break;
        }
        return date + " " + end;
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