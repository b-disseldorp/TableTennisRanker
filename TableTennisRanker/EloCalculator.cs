namespace TableTennisRanker;

public class EloCalculator
{
    // Function to calculate the Probability
    private static double Probability(int rating1, int rating2)
    {
        // Calculate and return the expected score
        return 1.0 / (1 + Math.Pow(10, (rating1 - rating2) / 400.0));
    }

    // Function to calculate Elo rating
    // K is a constant.
    // outcome determines the outcome: 1 for Player A win, 0 for Player B win, 0.5 for draw.
    public static void CalculateEloRating(ref int challenger, ref int defender, int K, double gameResult)
    {

        // Calculate the Winning Probability of Player A
        var winningProbabilityChallenger = Probability((int)defender, (int)challenger);

        // Calculate the Winning Probability of Player B
        var winningProbabilityDefender = Probability((int)challenger, (int)defender);

        // Update the Elo Ratings
        challenger = (int)(challenger + K * (gameResult - winningProbabilityChallenger));
        defender = (int)(defender + K * ((1 - gameResult) - winningProbabilityDefender));
    }
}
