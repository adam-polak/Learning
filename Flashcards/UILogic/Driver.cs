using DataAccessLibrary;

namespace UILogic;

public class Driver
{
    private static Dictionary<string, List<string>> menus = new Dictionary<string, List<string>>() 
    {
        { "Main", ["Add", "Delete", "View Set", "Practice", "View Scores", "Exit"] },
        { "Add", ["Add Set", "Add Element To Set", "Back"] },
        { "Delete", ["Delete Set", "Delete Element From Set", "Back"] }
    };
    private string lastKey;
    private string curKey;
    private ValidConnection connection;

    public Driver()
    {
        curKey = "Main";
        lastKey = "";
        connection = new ValidConnection();
    }

    private string PrintMenu()
    {
        return PrintInfo.PrintOptions(curKey, CurList());
    }

    private string PrintMenu(string prompt, List<string> list)
    {
        return PrintInfo.PrintOptions(curKey, prompt, list);
    }

    public void Run()
    {
        string command = "";
        while(!command.Equals("Exit"))
        {
            Console.Clear();
            command = PrintMenu();
            if(command.Equals("Back")) {
                curKey = lastKey;
                lastKey = curKey.Equals("Main") ? "" : "Main";
            } else if(menus.ContainsKey(command)) {
                lastKey = curKey;
                curKey = command;
            } else ExecCommand(command);
        }
    }

    private List<string> CurList()
    {
        List<string>? temp = menus.GetValueOrDefault(curKey);
        return temp == null ? new List<string>() : temp;
    }

    private string GetUserInput(string? line, List<string> list)
    {
        while(line == null || line.Length == 0 || list.Contains(line))
        {
            if(line == null || line.Length == 0)
            {
                Console.WriteLine("You must enter something...\n\n");
            } else Console.WriteLine("The set already contains \"" + line + "\"...\n\n");
            line = Console.ReadLine();
        }
        return line;
    }

    private string GetUserInput(string? line)
    {
        return GetUserInput(line, new List<string>());
    }

    private int GetNumber(string line)
    {
        char[] arr = line.ToCharArray();
        int raise = 0;
        int ans = 0;
        for(int i = arr.Length - 1; i >= 0; i--)
        {
            if(arr[i] >= '0' && arr[i] <= '9') ans += (arr[i] - '0') * (int)Math.Pow(10, raise++);
            else return 0;
        }
        return ans;
    }

    private void ExecCommand(string command)
    {
        List<string> list = new List<string>();
        List<Card> cards;
        string type;
        string userInput;
        string promptMessage;
        string exitString = "---Select To Exit---";
        switch(command)
        {
            case "Add Set":
                PrintInfo.PrintStackNames(CardStackController.Read(connection));
                foreach(CardStack cardStack in CardStackController.Read(connection)) list.Add(cardStack.Name);
                promptMessage = "\n\nEnter what the name of the flashcard stack you want to have will be:\n";
                Console.WriteLine(promptMessage);
                CardStackController.Insert(GetUserInput(Console.ReadLine(), list), connection);
                break;
            case "Add Element To Set":
                foreach(CardStack cardStack in CardStackController.Read(connection)) list.Add(cardStack.Name);
                if(list.Count == 0)
                {
                    Console.WriteLine("***There are no flashcard sets***");
                    Console.WriteLine("\n\n(Press enter to exit)\n");
                    Console.ReadLine();
                    break;
                }
                type = PrintMenu("Which set would you like to add to?", list);
                cards = CardController.Read(type, connection);
                List<string> cardFront = new List<string>();
                foreach(Card card in cards) cardFront.Add(card.Front);
                PrintInfo.PrintCards(cards, type);
                promptMessage = "\n\nEnter what the front of the flashcard you want to add will have:\n";
                Console.WriteLine(promptMessage);
                userInput = GetUserInput(Console.ReadLine(), cardFront);
                Console.Clear();
                PrintInfo.PrintCards(cards, type);
                promptMessage = "\n\nEnter what the back of \"" + userInput + "\" will have:\n";
                Console.WriteLine(promptMessage);
                Card add = new Card() { Name = type, Front = userInput, Back = GetUserInput(Console.ReadLine()) };
                CardController.Insert(add, connection);
                break;
            case "Delete Set":
                foreach(CardStack cardStack in CardStackController.Read(connection)) list.Add(cardStack.Name);
                if(list.Count == 0) 
                {
                    Console.WriteLine("***There are no flashcard sets***");
                    Console.WriteLine("\n\n(Press enter to exit)\n");
                    Console.ReadLine();
                    break;
                }
                list.Add(exitString);
                userInput = PrintInfo.PrintOptions(command, list);
                if(!userInput.Equals(exitString)) 
                {
                    CardController.DeleteAll(userInput, connection);
                    CardScoreController.DeleteAll(userInput, connection);
                    CardStackController.Delete(userInput, connection);
                }
                break;
            case "Delete Element From Set":
                foreach(CardStack cardStack in CardStackController.Read(connection)) list.Add(cardStack.Name);
                if(list.Count == 0)
                {
                    Console.WriteLine("***There are no flashcard sets***");
                    Console.WriteLine("\n\n(Press enter to exit)\n");
                    Console.ReadLine();
                    break;
                }
                list.Add(exitString);
                type = PrintInfo.PrintOptions("Which set would you like to delete from?", list);
                if(!type.Equals(exitString))
                {
                    Console.Clear();
                    PrintInfo.PrintCards(CardController.Read(type, connection), type);
                    promptMessage = "\n\nEnter the ID of the flashcard you would like to delete:\n";
                    Console.WriteLine(promptMessage);
                    int id = GetNumber(GetUserInput(Console.ReadLine()));
                    while(!CardController.ContainsId(id, type, connection))
                    {
                        Console.WriteLine("Enter a valid ID...\n");
                        id = GetNumber(GetUserInput(Console.ReadLine()));
                    }
                    CardController.Delete(id, type, connection);
                }
                break;
            case "View Set":
                foreach(CardStack cardStack in CardStackController.Read(connection)) list.Add(cardStack.Name);
                if(list.Count == 0)
                {
                    Console.WriteLine("***There are no flashcard sets***");
                    Console.WriteLine("\n\n(Press enter to exit)\n");
                    Console.ReadLine();
                    break;
                }
                type = PrintInfo.PrintOptions("Which set would you like to view?", list);
                PrintInfo.PrintCards(CardController.Read(type, connection), type);
                promptMessage = "\n\n(Press enter to exit)\n";
                Console.WriteLine(promptMessage);
                Console.ReadLine();
                break;
            case "Practice":
                foreach(CardStack cardStack in CardStackController.Read(connection)) list.Add(cardStack.Name);
                if(list.Count == 0)
                {
                    Console.WriteLine("***There are no flashcard sets***");
                    Console.WriteLine("\n\n(Press enter to exit)\n");
                    Console.ReadLine();
                    break;
                }
                type = PrintInfo.PrintOptions("Which set would you like to practice?", list);
                cards = CardController.Read(type, connection);
                if(cards.Count == 0)
                {
                    Console.WriteLine("***There are no cards to practice***");
                    Console.WriteLine("\n\n(Press enter to exit)\n");
                    Console.ReadLine();
                    break;
                }
                Console.WriteLine("Enter what the back of the card contains\n\nPress enter to start...");
                Console.ReadLine();
                int total = cards.Count;
                int score = 0;
                Random rnd = new Random();
                int index;
                Card c;
                string? ans;
                while(cards.Count > 0)
                {
                    Console.Clear();
                    index = rnd.Next(cards.Count);
                    c = cards.ElementAt(index);
                    PrintInfo.PrintCard(c);
                    ans = Console.ReadLine();
                    if(ans != null && ans.Equals(c.Back)) score++;
                    cards.RemoveAt(index);
                }
                Console.Clear();
                CardScore cardScore = new CardScore() { Name = type, Date = DateTime.Now.ToShortDateString(), Score = score + "/" + total };
                Console.WriteLine("You scored: " + cardScore.Score);
                Console.WriteLine("\n\n(Press enter to exit)\n");
                Console.ReadLine();
                CardScoreController.Insert(cardScore, connection);
                break;
            case "View Scores":
                foreach(CardStack cardStack in CardStackController.Read(connection)) list.Add(cardStack.Name);
                if(list.Count == 0)
                {
                    Console.WriteLine("***There are no flashcard sets***");
                    Console.WriteLine("\n\n(Press enter to exit)\n");
                    Console.ReadLine();
                    break;
                }
                type = PrintInfo.PrintOptions("Which set would you like to view?", list);
                PrintInfo.PrintScores(CardScoreController.Read(type, connection), type);
                promptMessage = "\n\n(Press enter to exit)\n";
                Console.WriteLine(promptMessage);
                Console.ReadLine();
                break;
        }
    }
}