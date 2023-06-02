using System.Xml.Serialization;
using MastermindCore.Entity;

namespace MastermindCore.Service.FileDatabase;

public class FileRatingService : IRatingService
{
    private const string FileName = "ratings.xml";
    private Dictionary<Player, Rating> _ratings = new();

    public FileRatingService()
    {
        LoadFile();
    }

    /// <inheritdoc />
    public bool Add(Rating rating)
    {
        ArgumentNullException.ThrowIfNull(rating.Player);
        var addedNewRecord = !_ratings.ContainsKey(rating.Player);
        _ratings[rating.Player] = rating;
        SaveFile();

        return addedNewRecord;
    }

    /// <inheritdoc />
    public IList<Rating> GetAll() => _ratings.Values.ToList();

    /// <inheritdoc />
    public Rating? GetByName(string name)
    {
        var player = _ratings.Keys.FirstOrDefault(p => p.Name == name);
        return player is null ? null : _ratings[player];
    }

    /// <inheritdoc />
    public Rating? GetByPlayer(Player player) => _ratings.ContainsKey(player) ? _ratings[player] : null;

    /// <inheritdoc />
    public double GetAvg() => _ratings.Values.Count == 0 ? 0 : _ratings.Values.Average(r => r.Stars);

    /// <inheritdoc />
    public void Reset()
    {
        _ratings.Clear();
        File.Delete(FileName);
    }

    private void SaveFile()
    {
        if (File.Exists(FileName)) 
            File.Delete(FileName);

        using var fs = File.OpenWrite(FileName);
        var serializer = new XmlSerializer(typeof(List<Rating>));
        serializer.Serialize(fs, _ratings.Values.ToList());
    }

    private void LoadFile()
    {
        if (!File.Exists(FileName))
        {
            _ratings = new Dictionary<Player, Rating>();
            return;
        }

        using var fs = File.OpenRead(FileName);
        var serializer = new XmlSerializer(typeof(List<Rating>));
        if (serializer.Deserialize(fs) is List<Rating> list)
        {
            _ratings = new Dictionary<Player, Rating>();

            foreach (var rating in list.Where(rating => !_ratings.ContainsKey(rating.Player!)))
            {
                ArgumentNullException.ThrowIfNull(rating.Player);
                _ratings.Add(rating.Player, rating);
            }
        }
    }
}