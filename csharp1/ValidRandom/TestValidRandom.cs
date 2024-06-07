namespace CSharp1;

public static class TestValidRandom {
    public static void RunTests() {
        bool check = true;
        int[] nums;
        Console.WriteLine("------------");
        Console.WriteLine("Testing Add");
        for(int i = 0; i < 10; i++) {
            nums = ValidRandom.GetRandomToAdd();
            Console.WriteLine(nums[0] + " " + nums[1]);
            if(nums[0] + nums[1] > 100) check = false;
        }
        if(check) Console.WriteLine("Add passed checks");
        else Console.WriteLine("Error in Add");
        Console.WriteLine("------------");

        Console.WriteLine("------------");
        Console.WriteLine("Testing Subtract");
        check = true;
        for(int i = 0; i < 10; i++) {
            nums = ValidRandom.GetRandomToSubtract();
            Console.WriteLine(nums[0] + " " + nums[1]);
            if(nums[0] - nums[1] < 0) check = false;
        }
        if(check) Console.WriteLine("Subtract passed checks");
        else Console.WriteLine("Error in Subtract");
        Console.WriteLine("------------");

        Console.WriteLine("------------");
        Console.WriteLine("Testing Multiply");
        for(int i = 0; i < 10; i++) {
            nums = ValidRandom.GetRandomToMultiply();
            Console.WriteLine(nums[0] + " " + nums[1]);
        }
        Console.WriteLine("------------");

        Console.WriteLine("------------");
        Console.WriteLine("Testing Divide");
        check = true;
        for(int i = 0; i < 10; i++) {
            nums = ValidRandom.GetRandomToDivide();
            Console.WriteLine(nums[0] + " " + nums[1]);
            if(nums[0] % nums[1] != 0) check = false;
        }
        if(check) Console.WriteLine("Divide passed checks");
        else Console.WriteLine("Error in Divide");
        Console.WriteLine("------------");

    }
}