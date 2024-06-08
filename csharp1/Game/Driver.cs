using System.Diagnostics;

namespace CSharp1;

public class Driver {
    private bool play = true;
    private int numRounds = 3;
    private Random rand = new Random();

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
            if(x == 7) play = false;
            else if(x == 6) DisplayHistory();
            else {
                long time = PlayType(x);
                Game g = new Game();
                g.TypeOfGame = TypeOfGame(x);
                g.LengthOfGame = time;
                g.Id = Game.NextId++;
                GameHistory.GetGames().Add(g);
            }
        }
    }

    private void DisplayHistory() {
        Console.Clear();
        LBreak();
        Console.WriteLine("   Id   |   Game Type   | Seconds   ");
        LBreak();
        foreach(Game g in GameHistory.GetGames()) {
            Console.WriteLine("   " + g.Id + "          " + g.TypeOfGame + "            " + g.LengthOfGame);
        }
        if(GameHistory.GetGames().ToArray().Length != 0) LBreak();
        Console.WriteLine("\n\n\n\nPress enter to escape...");
        string? wait = Console.ReadLine();
    }

    private string TypeOfGame(int x) {
        switch(x) {
            case 1: return "Add";
            case 2: return "Subtract";
            case 3: return "Multiplication";
            case 4: return "Division";
            case 5: return "Mixed";
        }
        return "";
    }

    private long PlayType(int x) {
        bool mix = x == 5;
        int round = 0;
        int target = 0;
        LBreak();
        Console.WriteLine("Perform the calculations that appear on the screen.");
        Console.WriteLine("Press enter to start....");
        LBreak();
        string? wait = Console.ReadLine();
        Stopwatch watch = Stopwatch.StartNew();
        while(round < numRounds) {
            Console.Clear();
            Console.WriteLine("Round " + round + "/" + numRounds);
            int[]? check;
            bool checkAns = false;
            if(mix) x = rand.Next(1, 5);
            switch(x) {
                case 1:
                    check = ValidRandom.GetRandomToAdd();
                    Console.WriteLine(check[0] + " + " + check[1] + " = ?");
                    target = check[0] + check[1];
                    while(checkAns == false) {
                        checkAns = ValidateAnswer(target, Console.ReadLine());
                        if(!checkAns) Console.WriteLine("Wrong answer try again!");
                    }
                    break;
                case 2:
                    check = ValidRandom.GetRandomToSubtract();
                    Console.WriteLine(check[0] + " - " + check[1] + " = ?");
                    target = check[0] - check[1];
                    while(checkAns == false) {
                        checkAns = ValidateAnswer(target, Console.ReadLine());
                        if(!checkAns) Console.WriteLine("Wrong answer try again!");
                    }
                    break;
                case 3:
                    check = ValidRandom.GetRandomToMultiply();
                    Console.WriteLine(check[0] + " * " + check[1] + " = ?");
                    target = check[0] * check[1];
                    while(checkAns == false) {
                        checkAns = ValidateAnswer(target, Console.ReadLine());
                        if(!checkAns) Console.WriteLine("Wrong answer try again!");
                    }
                    break;
                case 4:
                    check = ValidRandom.GetRandomToDivide();
                    Console.WriteLine(check[0] + " / " + check[1] + " = ?");
                    target = check[0] / check[1];
                    while(checkAns == false) {
                        checkAns = ValidateAnswer(target, Console.ReadLine());
                        if(!checkAns) Console.WriteLine("Wrong answer try again!");
                    }
                    break;
            }
            round++;
        }
        watch.Stop();
        return watch.ElapsedMilliseconds / 1000;
    }

    private bool ValidateAnswer(int target, string? s) {
        if(s == null || s.Length == 0 || s.Length >= 4) return false;
        char[] arr = s.ToCharArray();
        int add = 0;
        int check = 0;
        int raise = 0;
        for(int i = arr.Length - 1; i >= 0; i--) {
            add = (arr[i] - '0') * (int)Math.Pow(10, raise);
            check += add;
            raise++;
        }
        return check == target;
    }

    private int ValidateMenuChoice(string? s) {
        if(s == null || s.Length != 1) return InvalidInput();
        char[] arr = s.ToCharArray();
        int check = arr[0] - '0';
        if(check > 0 && check <= 7) return check;
        else return InvalidInput();
    }

    private int InvalidInput() {
        InvalidMainMenu();
        return 0;
    }

    private void InvalidMainMenu() {
        LBreak();
        Console.WriteLine("Invalid Input: Enter a number 1-7 based on the options presented above");
        LBreak();
    }

    private void LBreak() {
        Console.WriteLine("-------------------------------------------------------------");
    }

    private void PresentOptions() {
        LBreak();
        Console.WriteLine("What would you like to do? (Enter a number 1-7)");
        LBreak();
        Console.WriteLine("1. Play adding mode");
        Console.WriteLine("2. Play subtraction mode");
        Console.WriteLine("3. Play multiplication mode");
        Console.WriteLine("4. Play division mode");
        Console.WriteLine("5. Play combined mode (utilizes each operator above at random)");
        Console.WriteLine("6. Display game history");
        LBreak();
        Console.WriteLine("7. Exit");
        LBreak();
    }

}