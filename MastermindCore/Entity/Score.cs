using System.ComponentModel.DataAnnotations.Schema;

namespace MastermindCore.Entity;

/// <summary>
/// Predstavuje skore v hre, ktore dosiahol hrac.
/// </summary>
[Serializable]
public class Score
{
    /// <summary>
    /// Vrati alebo nastavi identifikator skore.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Vrati alebo nastavi identifikator hraca (cudzi kluc).
    /// </summary>
    public string PlayerName { get; set; } = "";

    /// <summary>
    /// Vrati alebo nastavi hraca, ktory dosiahol toto skore.
    /// </summary>
    [ForeignKey(nameof(PlayerName))]
    public virtual Player? Player { get; set; }

    /// <summary>
    /// Vrati alebo nastavi skore ako cislo.
    /// </summary>
    public int Points { get; set; }

    /// <summary>
    /// Vrati alebo nastavi datum a cas odohrania hry s danym skore danym hracom.
    /// </summary>
    public DateTime PlayedAt { get; set; }

    /// <summary>
    /// Vrati alebo nastavi dlzku trvania hry s danym skore danym hracom (ak vyhral alebo prehral).
    /// </summary>
    public TimeSpan Duration { get; set; }
}