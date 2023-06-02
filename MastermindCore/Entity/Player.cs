using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace MastermindCore.Entity;

public class Player
{
    /// <summary>
    /// Vrati alebo nastavi meno hraca (primarny kluc).
    /// </summary>
    [Key]
    public string Name { get; set; } = "";

    /// <summary>
    /// Vrati alebo nastavi heslo hraca.
    /// </summary>
    public string PasswordHash { get; set; } = "";

    /// <summary>
    /// Vrati alebo nastavi datum a cas registrovania hraca.
    /// </summary>
    public DateTime RegisteredAt { get; set; }

    [XmlIgnore]
    [JsonIgnore]
    public virtual ICollection<Score>? Scores { get; set; }

    [XmlIgnore]
    [JsonIgnore]
    public virtual Rating? Rating { get; set; }

    [XmlIgnore]
    [JsonIgnore]
    public virtual ICollection<Comment>? Comments { get; set; }

    public Player()
    {
    }

    public Player(string name, string password = "", DateTime registeredAt = default)
    {
        Name = name;
        PasswordHash = Encryptor.Md5Hash(password);
        RegisteredAt = registeredAt;
    }

    /// <inheritdoc />
    public override string ToString() => Name;
}