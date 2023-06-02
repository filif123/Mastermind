using MastermindCore.Entity;

namespace MastermindCore.Service;

public interface IScoreService
{
    /// <summary>
    /// Prida novy komentar do databazy.
    /// </summary>
    /// <param name="score">skore</param>
    void Add(Score score);

    /// <summary>
    /// Vrati 3 najlepsich hracov s ich skore z databazy.
    /// </summary>
    /// <returns></returns>
    IList<Score> GetTopThree();

    /// <summary>
    /// Vrati vsetky skore z databazy.
    /// </summary>
    /// <returns></returns>
    IList<Score> GetAll();

    /// <summary>
    /// Odstrani vsetky skore z databazy.
    /// </summary>
    void Reset();
}