namespace TableTennisRanker.Data;

public class Competitor()
{
    public Competitor(int id, string firstName, string lastName, int eloPoints, int gamesPlayed) : this()
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        EloPoints = eloPoints;
        GamesPlayed = gamesPlayed;
    }
    public int Id { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public int EloPoints { get; set; } = 1000;
    public int GamesPlayed{ get; set; }

    public override string ToString()
    {
        return FirstName + " " + LastName;
    }
}
