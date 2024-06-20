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
        return "";
    }

    public string EndShift(string username, int key)
    {
        return "";
    }

    public List<Shift> ViewShifts(string username, int key)
    {
        return new List<Shift>();
    }
}