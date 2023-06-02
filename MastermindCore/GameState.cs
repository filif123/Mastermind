namespace MastermindCore;

/// <summary>
/// Enumeracia reprezentujuca stav hry.
/// </summary>
public enum GameState
{
    /// <summary>
    /// Hra este nezacala.
    /// </summary>
    NotStarted,

    /// <summary>
    /// Hra prebieha.
    /// </summary>
    Playing,

    /// <summary>
    /// Hra bola ukoncena s vyhrou.
    /// </summary>
    Solved,

    /// <summary>
    /// Hra bola ukoncena s prehrou.
    /// </summary>
    Lose
}