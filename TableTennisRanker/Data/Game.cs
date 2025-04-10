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
    public int Id { get; init; }
    public Competitor Challenger { get; init; } = null!;
    public Competitor Defender { get; init; } = null!;
    public int ScoreChallenger { get; init; }
    public int ScoreDefender { get; init; }
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
        double gameResult;
        if (ScoreChallenger > ScoreDefender)
        {
            var score = (double)ScoreDefender / ScoreChallenger/2;
            gameResult = 1-score;
        }
        else
        {
            var score = (double)ScoreChallenger / ScoreDefender/2;
            gameResult = 0+score;
        }
        CalculateEloRating(ref challengerFutureEloPoints, ref defenderFutureEloPoints, 30, gameResult);
        ChallengerEloPoints = challengerFutureEloPoints - challenger.EloPoints;
        DefenderEloPoints = defenderFutureEloPoints - defender.EloPoints;
        challenger.EloPoints = challengerFutureEloPoints;
        defender.EloPoints = defenderFutureEloPoints;

        challenger.GamesPlayed++;
        defender.GamesPlayed++;
    }

    public void RemovePoints(CompetitorManager competitorManager)
    {
        var challenger = competitorManager.GetCompetitor(Challenger.ToString());
        var defender = competitorManager.GetCompetitor(Defender.ToString());

        if (challenger == null || defender == null)
        {
            return;
        }
        challenger.GamesPlayed--;
        defender.GamesPlayed--;

        challenger.EloPoints -= ChallengerEloPoints;
        defender.EloPoints -= DefenderEloPoints;
    }

    // Function to calculate Elo rating
    // K is a constant.
    // outcome determines the outcome: 1 for Player A win, 0 for Player B win, 0.5 for draw.
    private static void CalculateEloRating(ref int challenger, ref int defender, int k, double gameResult)
    {

        // Calculate the Winning Probability
        var winningProbabilityChallenger = Probability(defender, challenger);

        var temp = (int)(k * (gameResult - winningProbabilityChallenger));
        var temp2 = temp * -1;


        challenger += temp;
        defender += temp2;
    }

    // Function to calculate the Probability
    private static double Probability(int rating1, int rating2)
    {
        // Calculate and return the expected score
        return 1.0 / (1 + Math.Pow(10, (rating1 - rating2) / 400.0));
    }

    public override string ToString()
    {
        return Id + ": " + Challenger + " v " + Defender;
    }
}
