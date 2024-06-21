using System.Runtime.InteropServices;
using ShiftLoggerUI.Controllers;
using Spectre.Console;

namespace ShiftLoggerUI;

public class Driver
{
    private UserController userController;
    private ShiftController shiftController;
    private bool loggedIn;
    private string username;
    private int session_key;

    public Driver()
    {
        userController = new UserController();
        shiftController = new ShiftController();
        loggedIn = false;
        username = "";
    }

    public void Run()
    {

    }

    private void LoginScreen()
    {
        if(loggedIn) return;
        string choice = PrintOptions("What would you like to do?", ["Login", "Create User"]);
        if(choice.Equals("Login")) RunLogin();
        else RunCreateUser();
    }

    private void RunLogin()
    {
        
    }

    private void RunCreateUser()
    {

    }

    private string PrintOptions(string title, List<string> commands)
    {
        string? select = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title(title)
            .AddChoices(commands)
        );
        return select ?? "";
    }
}