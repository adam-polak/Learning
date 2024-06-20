// See https://aka.ms/new-console-template for more information
using ShiftLoggerUI.Controllers;

Console.WriteLine("Hello, World!");

UserController controller = new UserController();

int key = controller.Login("admin", "testing");
Console.WriteLine(key);
Console.WriteLine(controller.IsCorrectLogin("admin", key));
Console.WriteLine(controller.Logout("admin", key));
Console.WriteLine(controller.IsCorrectLogin("admin", key));