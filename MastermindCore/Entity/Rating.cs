using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MastermindCore.Entity;

/// <summary>
/// Predstavuje hodnotenie, ktore vytvoril hrac.
/// </summary>
[Serializable]
public class Rating
{
    private int _stars;

    /// <summary>
    /// Vrati alebo nastavi identifikator hraca (cudzi kluc).
    /// </summary>
    [Key]
    public string PlayerName { get; set; } = "";

    /// <summary>
    /// Vrati alebo nastavi hraca, ktory ohodnotil hru.
    /// </summary>
    [ForeignKey(nameof(PlayerName))]
    public virtual Player? Player { get; set; }

    /// <summary>
    /// Vrati alebo nastavi pocet hviezdiciek hodnotenia.
    /// </summary>
    public int Stars
    {
        get => _stars;
        set
        {
            if (value is < 1 or > 5)
                throw new ArgumentException("Count of stars must be between <1,5>.");
            _stars = value;
        }
    }

    /// <summary>
    /// Vrati alebo nastavi datum a cas uverejnenia hodnotenia.
    /// </summary>
    public DateTime RatedAt { get; set; }
}