namespace UILogic;

public class Driver
{
    private bool run;
    private Menu menu;

    public Driver()
    {
        run = true;
        menu = new Menu();
    }

    public void Run()
    {
        int input;
        int[] range;
        while(run)
        {
            input = 0;
            range = menu.GetCommandRange();
            while(input < range[0] || input > range[1])
            {
                input = GetInput(Console.ReadLine());
            }
            if(input == menu.GetExitVal()) run = false;
            else menu.ExecCommand(input);
        }
    }

    private int GetInput(string? line)
    {
        if(line == null || line.Length != 1) return 0;
        char[] arr = line.ToCharArray();
        int ans = 0;
        int raise = 0;
        for(int i = arr.Length - 1; i >= 0; i--)
        {
            if(arr[i] >= '0' && arr[i] <= '9') ans += (arr[i] - '0') * (int)Math.Pow(10, raise++);
            else return 0;
        }
        return ans;
    }
}