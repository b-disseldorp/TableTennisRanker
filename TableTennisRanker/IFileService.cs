using TableTennisRanker.Data;

namespace TableTennisRanker;

public interface IFileService
{
    public Task SaveCompetitors(List<Competitor?> competitors);
    public Task<List<Competitor?>?> LoadCompetitors();
    public Task SaveGames(List<Game> games);
    public Task<List<Game>?> LoadGames();
}
