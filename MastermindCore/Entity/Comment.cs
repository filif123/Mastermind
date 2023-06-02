using System.ComponentModel.DataAnnotations.Schema;

namespace MastermindCore.Entity;

/// <summary>
/// Predstavuje komentar, ktore napisal hrac.
/// </summary>
[Serializable]
public class Comment
{
    /// <summary>
    /// Vrati alebo nastavi identifikator komentaru.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Vrati alebo nastavi identifikator hraca (cudzi kluc).
    /// </summary>
    public string PlayerName { get; set; } = "";

    /// <summary>
    /// Vrati alebo nastavi hraca, ktory okomentoval hru.
    /// </summary>
    [ForeignKey(nameof(PlayerName))]
    public virtual Player? Player { get; set; }

    /// <summary>
    /// Vrati alebo nastavi text komentaru.
    /// </summary>
    public string Text { get; set; } = "";

    /// <summary>
    /// Vrati alebo nastavi datum a cas uverejnenia komentaru.
    /// </summary>
    public DateTime CommentedAt { get; set; }
}