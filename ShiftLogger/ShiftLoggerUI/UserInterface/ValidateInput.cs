namespace ShiftLoggerUI;

public static class ValidateInput
{
    public static int GetInt(string? input)
    {
        if(input == null || input.Count() == 0) return -1;
        int raise = 0;
        int ans = 0;
        char[] arr = input.ToCharArray();
        for(int i = arr.Length - 1; i >= 0; i--)
        {
            if(arr[i] >= '0' && arr[i] <= '9') ans += (arr[i] - '0') * (int)Math.Pow(10, raise++);
            else return -1;
        }
        return ans;
    }
}