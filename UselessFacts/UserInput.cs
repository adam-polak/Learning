using UselessFacts.Models;

namespace UselessFacts;

public class UserInput
{
    private FactController factController;

    public UserInput()
    {
        factController = new FactController();
    }

    public void RunMain()
    {
        Console.Clear();
        string response = PrintUI.PrintOptions(["View Daily Fact", "View Random Fact", "--Exit--"], "What would you like to do?");
        switch(response)
        {
            case "View Daily Fact":
                ViewDailyFact();
                break;
            case "View Random Fact":
                ViewRandomFact();
                break;
            case "--Exit--":
                return;
        }
    }

    private void ViewDailyFact()
    {
        PrintUI.PrintFact(factController.GetDailyFact(), "Daily Fact");
        RunMain();
    }

    private void ViewRandomFact()
    {
        PrintUI.PrintFact(factController.GetRandomFact(), "Random Fact");
        RunMain();
    }
}