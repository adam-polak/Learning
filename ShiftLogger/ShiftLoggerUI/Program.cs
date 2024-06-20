// See https://aka.ms/new-console-template for more information
using Npgsql;
using ShiftLoggerUI.Controllers;

NpgsqlConnection connection = new NpgsqlConnection("Host=localhost:5432;Username=postgres;Password=password;Database=shiftloggerdb;");
connection.Open();
NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM shift_table;", connection);
cmd.ExecuteNonQuery();
cmd = new NpgsqlCommand("DELETE FROM user_table WHERE username='testUser';", connection);
cmd.ExecuteNonQuery();

UserController userController = new UserController();
ShiftController shiftController = new ShiftController();
string username = "testUser";
string password = "testing";
userController.CreateUser(username, password);
int key = userController.Login(username, password);
shiftController.StartShift(username, key);
shiftController.EndShift(username, key);
shiftController.ViewShifts(username, key);
userController.Logout(username, key);