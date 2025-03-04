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

    public void GivePoints(CompetitorManager competitorManager)
    {
        var challenger = competitorManager.GetCompetitor(Challenger.ToString());
        var defender = competitorManager.GetCompetitor(Defender.ToString());

        if (challenger == null || defender == null)
        {
            throw new Exception();
        }
        var cTemp = challenger.Points;
        var dTemp = defender.Points;
        if (ScoreChallenger > ScoreDefender)
        {
            EloCalculator.CalculateEloRating(ref cTemp, ref dTemp, 30, 1);
        }
        else
        {
            EloCalculator.CalculateEloRating(ref cTemp, ref dTemp, 30, 0);
        }
        challenger.Points = cTemp;
        defender.Points = dTemp;

        challenger.GamesPlayed++;
        defender.GamesPlayed++;
    }
}
