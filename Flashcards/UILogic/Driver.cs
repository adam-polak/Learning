using DataAccessLibrary;

namespace UILogic;

public class Driver
{
    private bool run;
    private ValidConnection validConnection;

    public Driver()
    {
        run = true;
        validConnection = new ValidConnection();
    }

    public void Run()
    {
        int input;
        while(run)
        {
            input = 0;
            while(input == 0)
            {
                input = GetInput(Console.ReadLine());
            }
            if(input == 1) run = false;
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