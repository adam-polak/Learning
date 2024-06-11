namespace DataAccessLibrary;

public class DataController
{

    private string type;
    private Table table;


    public DataController(string type, Table table) 
    {
        this.type = type;
        this.table = table;
    }

    public void InsertStack() 
    {

    }

    public void InsertCard()
    {

    }

    public void InsertScore()
    {
        
    }

    public void Update(int id) 
    {
        if(table == Table.Stacks) return;
    }

    public void Delete(string t) 
    {
        if(table != Table.Stacks) return;
    }

    public void Delete(int id) 
    {

    }

    public List<FlashCardStack>? ReadStacks() 
    {
        if(table != Table.Stacks) return null;
        return new List<FlashCardStack>();
    }

    public List<FlashCard>? ReadCards() 
    {
        if(table != Table.Cards) return null;
        return new List<FlashCard>();
    }

    public List<FlashCardScore> ReadScores() 
    {
        if(table != Table.Scores) return null;
        return new List<FlashCardScore>();
    }

}

public enum Table 
{
    Stacks,
    Cards,
    Scores
}