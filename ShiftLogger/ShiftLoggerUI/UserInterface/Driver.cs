using ShiftLoggerUI.Controllers;

namespace ShiftLoggerUI;

public class Driver
{
    private UserController userController;
    private ShiftController shiftController;

    public Driver()
    {
        userController = new UserController();
        shiftController = new ShiftController();
    }
}