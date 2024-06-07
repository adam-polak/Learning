using System.Collections;

namespace CSharp1;

public static class ValidRandom {

    private static Random rand = new Random();
    private static int[] ans = new int[2];

    public static int[] GetRandomToAdd() {
        int sum = rand.Next(101);
        ans[0] = rand.Next(sum);
        ans[1] = sum - ans[0];
        return ans;
    }

    public static int[] GetRandomToSubtract() {
        ans[0] = rand.Next(101);
        ans[1] = rand.Next(ans[0]);
        return ans;
    }

    public static int[] GetRandomToMultiply() {
        ans[0] = rand.Next(11);
        ans[1] = rand.Next(11);
        return ans;
    }

    public static int[] GetRandomToDivide() {
        ans[0] = rand.Next(101);
        Object?[] factors = FindFactors(ans[0]);
#pragma warning disable CS8605 // Unboxing a possibly null value.
        ans[1] = (int)factors[rand.Next(factors.Length)];
#pragma warning restore CS8605 // Unboxing a possibly null value.
        return ans;
    }

    private static Object?[] FindFactors(int x) {
        ArrayList list = [1, x];
        int check;
        if(x % 2 == 0) check = 2;
        else check = 3;
        while(check < x / 2) {
            if(x % check == 0) {
                list.Add(check);
                list.Add(x / check);
            }
            check += 2;
        }
        return list.ToArray();
    }

}