using System.Xml.Serialization;
using MastermindCore.Entity;

namespace MastermindCore.Service.FileDatabase;

public class FileCommentService : ICommentService
{
    private const string FileName = "comments.xml";
    private IList<Comment> _comments = new List<Comment>();

    public FileCommentService()
    {
        LoadFile();
    }

    /// <inheritdoc />
    public void Add(Comment comment)
    {
        comment.Id = _comments.Count;
        _comments.Add(comment);
        SaveFile();
    }

    public IList<Comment> GetAll() => _comments;

    /// <inheritdoc />
    public void Reset()
    {
        _comments.Clear();
        File.Delete(FileName);
    }

    private void SaveFile()
    {
        if (File.Exists(FileName)) 
            File.Delete(FileName);

        using var fs = File.OpenWrite(FileName);
        var serializer = new XmlSerializer(typeof(List<Comment>));
        serializer.Serialize(fs, _comments);
    }

    private void LoadFile()
    {
        if (!File.Exists(FileName))
        {
            _comments = new List<Comment>();
            return;
        }

        using var fs = File.OpenRead(FileName);
        var serializer = new XmlSerializer(typeof(List<Comment>));
        if (serializer.Deserialize(fs) is List<Comment> list)
            _comments = list;
    }
}