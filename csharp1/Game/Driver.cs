namespace CSharp1;

public class Driver {
    private bool play = true;
    private string univDash = "---------------------------";

    public void RunGame() {
        PresentOptions();
        ValidateMenuChoice(Console.ReadLine());
    }

    private int ValidateMenuChoice(string? s) {
        if(s == null || s.Length != 1) return InvalidInput();
        char[] arr = s.ToCharArray();
        int check = arr[0] - '0';
        if(check > 0 && check <= 5) return check;
        else return InvalidInput();
    }

    public bool IsGameRunning() {
        return play;
    }

    private int InvalidInput() {
        return 0;
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