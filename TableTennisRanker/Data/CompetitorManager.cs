namespace TableTennisRanker.Data;

public class CompetitorManager
{
    public List<Competitor?> Competitors = [];

    private Competitor? GetCompetitor(int competitorId)
    {
        return Competitors[competitorId];
    }

    public Competitor? GetCompetitor(string competitorName)
    {
        return Competitors.FirstOrDefault(competitor => competitor.ToString() == competitorName);
    }

    public void RemoveCompetitor(Competitor competitor)
    {
        Competitors.Remove(competitor);
    }
}
