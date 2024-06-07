namespace CSharp1;

public class Driver {
    private bool play = true;
    private string univDash = "---------------------------";

    public void RunGame() {
        PresentOptions();
    }

    public bool IsGameRunning() {
        return play;
    }

    private void PresentOptions() {
        Console.WriteLine(univDash);
        Console.WriteLine("What would you like to do? (Enter a number 1-5)");
        Console.WriteLine(univDash);
        Console.WriteLine("1. Play adding mode");
        Console.WriteLine("2. Play subtraction mode");
        Console.WriteLine("3. Play multiplication mode");
        Console.WriteLine("4. Play division mode");
        Console.WriteLine(univDash);
        Console.WriteLine("5. Exit");
        Console.WriteLine(univDash);
    }

}