using System.Xml.Serialization;
using MastermindCore.Entity;

namespace MastermindCore.Service.FileDatabase;

public class FileScoreService : IScoreService
{
    private const string FileName = "scores.xml";
    private IList<Score> _scores = new List<Score>();

    public FileScoreService()
    {
        LoadFile();
    }

    /// <inheritdoc />
    public void Add(Score score)
    {
        score.Id = _scores.Count;
        _scores.Add(score);
        SaveFile();
    }

    /// <inheritdoc />
    public IList<Score> GetTopThree() => _scores.OrderByDescending(s => s.Points).Take(3).ToList();

    /// <inheritdoc />
    public IList<Score> GetAll() => _scores;

    /// <inheritdoc />
    public void Reset()
    {
        _scores.Clear();
        File.Delete(FileName);
    }

    private void SaveFile()
    {
        if (File.Exists(FileName)) 
            File.Delete(FileName);

        using var fs = File.OpenWrite(FileName);
        var serializer = new XmlSerializer(typeof(List<Score>));
        serializer.Serialize(fs, _scores);
    }

    private void LoadFile()
    {
        if (!File.Exists(FileName))
        {
            _scores = new List<Score>();
            return;
        }

        using var fs = File.OpenRead(FileName);
        var serializer = new XmlSerializer(typeof(List<Score>));
        if (serializer.Deserialize(fs) is List<Score> list)
            _scores = list;
    }
}