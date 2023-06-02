using System.Xml.Serialization;
using MastermindCore.Entity;

namespace MastermindCore.Service.FileDatabase;

public class FilePlayerService : IPlayerService
{
    private const string FileName = "players.xml";
    private IList<Player> _players = new List<Player>();

    public FilePlayerService()
    {
        LoadFile();
    }

    /// <inheritdoc />
    public void Add(Player player)
    {
        if (_players.Any(p => p.Name == player.Name))
            throw new ArgumentException("Player is already in database.");
        _players.Add(player);
        SaveFile();
    }

    /// <inheritdoc />
    public Player? Get(string name, string password) => _players.FirstOrDefault(p => p.Name == name && p.PasswordHash == Encryptor.Md5Hash(password));

    /// <inheritdoc />
    public bool Exists(string name) => _players.Any(p => p.Name == name);

    /// <inheritdoc />
    public IList<Player> GetAll() => _players;

    /// <inheritdoc />
    public void Reset()
    {
        _players.Clear();
        File.Delete(FileName);
    }

    private void SaveFile()
    {
        if (File.Exists(FileName)) 
            File.Delete(FileName);

        using var fs = File.OpenWrite(FileName);
        var serializer = new XmlSerializer(typeof(List<Player>));
        serializer.Serialize(fs, _players);
    }

    private void LoadFile()
    {
        if (!File.Exists(FileName))
        {
            _players = new List<Player>();
            return;
        }

        using var fs = File.OpenRead(FileName);
        var serializer = new XmlSerializer(typeof(List<Player>));
        if (serializer.Deserialize(fs) is List<Player> list)
            _players = list;
    }
}