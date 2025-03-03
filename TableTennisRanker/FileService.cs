using System.IO.Abstractions;
using System.Text.Json;
using TableTennisRanker.Data;

namespace TableTennisRanker;

public class FileService(IFileSystem fileSystem) : IFileService
{
    private IFileSystem FileSystem { get; } = fileSystem;

    private const string path = @"C:\TTR";

    public async Task SaveCompetitors(List<Competitor> competitors)
    {
        await using var createStream = FileSystem.File.Create($@"{path}\competitors.json");
        await JsonSerializer.SerializeAsync(createStream, competitors);
    }

    public async Task<List<Competitor>?> LoadCompetitors()
    {
        const string jsonFile = $@"{path}\competitors.json";
        if (!FileSystem.File.Exists(jsonFile))
        {
            FileSystem.File.Create(jsonFile).Close();
            return [];
        }
        using var file = FileSystem.File.OpenText(jsonFile);
        try
        {
            return await JsonSerializer.DeserializeAsync<List<Competitor>>(file.BaseStream);
        }
        catch (JsonException)
        {
            return [];
        }
    }

    public async Task SaveGames(List<Game> games)
    {
        await using var createStream = FileSystem.File.Create($@"{path}\games.json");
        await JsonSerializer.SerializeAsync(createStream, games);
    }

    public async Task<List<Game>?> LoadGames()
    {
        const string jsonFile = $@"{path}\games.json";
        if (!FileSystem.File.Exists(jsonFile))
        {
            FileSystem.File.Create(jsonFile).Close();
            return [];
        }
        using var file = FileSystem.File.OpenText(jsonFile);
        try
        {
            return await JsonSerializer.DeserializeAsync<List<Game>>(file.BaseStream);
        }
        catch (JsonException)
        {
            return [];
        }
    }
}
