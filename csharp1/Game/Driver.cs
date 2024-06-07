namespace CSharp1;

public class Driver {
    private bool play = true;
    private int rounds = 10;

    public void RunGame() {
        while(play) {
            int x = 0;
            bool inv = false;
            while(x == 0) {
                Console.Clear();
                PresentOptions();
                if(inv) InvalidInput();
                x = ValidateMenuChoice(Console.ReadLine());
                inv = x == 0;
            }
            Console.Clear();
            if(x == 6) play = false;
        }
    }

    private void PlayType(int x) {
        if(x == 1) PlayAdd();
        else if(x == 2) PlaySubtract();
        else if(x == 3) PlayMultiply();
        else if(x == 4) PlayDivide();
        else if(x == 5) PlayMix();
    }

    private void PlayAdd() {
    }

    private void PlaySubtract() {

    }

    private void PlayMultiply() {

    }

    private void PlayDivide() {

    }

    private void PlayMix() {
        
    }

    private int ValidateMenuChoice(string? s) {
        if(s == null || s.Length != 1) return InvalidInput();
        char[] arr = s.ToCharArray();
        int check = arr[0] - '0';
        if(check > 0 && check <= 6) return check;
        else return InvalidInput();
    }

    private int InvalidInput() {
        InvalidMainMenu();
        return 0;
    }

    private void InvalidMainMenu() {
        LBreak();
        Console.WriteLine("Invalid Input: Enter a number 1-6 based on the options presented above");
        LBreak();
    }

    private void LBreak() {
        Console.WriteLine("------------------------------------------------------------------");
    }

    private void PresentOptions() {
        LBreak();
        Console.WriteLine("What would you like to do? (Enter a number 1-6)");
        LBreak();
        Console.WriteLine("1. Play adding mode");
        Console.WriteLine("2. Play subtraction mode");
        Console.WriteLine("3. Play multiplication mode");
        Console.WriteLine("4. Play division mode");
        Console.WriteLine("5. Play combined mode (utilizes each operator above at random)");
        LBreak();
        Console.WriteLine("6. Exit");
        LBreak();
    }

}