namespace UselessFacts;

public class UserInput
{
    public void MainMenu()
    {
        string response = PrintUI.PrintOptions(["View Daily Fact", "View Random Fact", "--Exit--"], "What would you like to do?");
        switch(response)
        {
            case "View Daily Fact":
                break;
            case "View Random Fact":
                break;
            case "--Exit--":
                return;
        }
    }

    private void ViewDailyFact()
    {

        MainMenu();
    }

    private void ViewRandomFact()
    {
        
        MainMenu();
    }
}