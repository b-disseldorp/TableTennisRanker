namespace TableTennisRanker.Data;

public class Game()
{

    public Game(int id, Competitor challenger, Competitor defender, int scoreChallenger, int scoreDefender) : this()
    {
        Id = id;
        Challenger = challenger;
        Defender = defender;
        ScoreChallenger = scoreChallenger;
        ScoreDefender = scoreDefender;
    }
    public int Id { get; set; }
    public Competitor Challenger { get; set; } = null!;
    public Competitor Defender { get; set; } = null!;
    public int ScoreChallenger { get; set; }
    public int ScoreDefender { get; set; }
}
