namespace TableTennisRanker.Data;

public class CompetitorManager
{
    public List<Competitor> Competitors = [];

    public Competitor GetCompetitor(int competitorId)
    {
        return Competitors[competitorId];
    }

    public Competitor? GetCompetitor(string competitorName)
    {
        return Competitors.FirstOrDefault(competitor => competitor.ToString() == competitorName);
    }
}
