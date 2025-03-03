namespace TableTennisRanker.Data;

public class Competitor()
{
    public Competitor(int id, string firstName, string lastName, int points, int gamesPlayed) : this()
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Points = points;
        GamesPlayed = gamesPlayed;
    }
    public int Id { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public int Points{ get; set; }
    public int GamesPlayed{ get; set; }

    public override string ToString()
    {
        return FirstName + " " + LastName;
    }
}
