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
    public int ChallengerEloPoints { get; set; }
    public int DefenderEloPoints { get; set; }

    public void GivePoints(CompetitorManager competitorManager)
    {
        var challenger = competitorManager.GetCompetitor(Challenger.ToString());
        var defender = competitorManager.GetCompetitor(Defender.ToString());

        if (challenger == null || defender == null)
        {
            throw new Exception();
        }
        var challengerFutureEloPoints = challenger.EloPoints;
        var defenderFutureEloPoints = defender.EloPoints;
        if (ScoreChallenger > ScoreDefender)
        {
            CalculateEloRating(ref challengerFutureEloPoints, ref defenderFutureEloPoints, 30, 1);
        }
        else
        {
            CalculateEloRating(ref challengerFutureEloPoints, ref defenderFutureEloPoints, 30, 0);
        }

        ChallengerEloPoints = challengerFutureEloPoints - challenger.EloPoints;
        DefenderEloPoints = defenderFutureEloPoints - defender.EloPoints;
        challenger.EloPoints = challengerFutureEloPoints;
        defender.EloPoints = defenderFutureEloPoints;

        challenger.GamesPlayed++;
        defender.GamesPlayed++;
    }

    // Function to calculate Elo rating
    // K is a constant.
    // outcome determines the outcome: 1 for Player A win, 0 for Player B win, 0.5 for draw.
    private static void CalculateEloRating(ref int challenger, ref int defender, int K, double gameResult)
    {

        // Calculate the Winning Probability of Player A
        var winningProbabilityChallenger = Probability(defender, challenger);

        // Calculate the Winning Probability of Player B
        var winningProbabilityDefender = Probability(challenger, defender);

        // Update the Elo Ratings
        challenger = (int)(challenger + K * (gameResult - winningProbabilityChallenger));
        defender = (int)(defender + K * ((1 - gameResult) - winningProbabilityDefender));
    }

    // Function to calculate the Probability
    private static double Probability(int rating1, int rating2)
    {
        // Calculate and return the expected score
        return 1.0 / (1 + Math.Pow(10, (rating1 - rating2) / 400.0));
    }
}
