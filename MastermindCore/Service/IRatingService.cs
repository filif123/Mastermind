using MastermindCore.Entity;

namespace MastermindCore.Service;

public interface IRatingService
{ 
    /// <summary>
    /// Prida nove hodnotenie do databazy.
    /// </summary>
    /// <param name="rating">hodnotenie</param>
    /// <returns></returns>
    bool Add(Rating rating);

    /// <summary>
    /// Vrati vsetky hodnotenia z databazy.
    /// </summary>
    /// <returns></returns>
    IList<Rating> GetAll();

    /// <summary>
    /// Vrati hodnotenie pouzivatela z databazy.
    /// </summary>
    /// <returns></returns>
    Rating? GetByName(string name);

    /// <summary>
    /// Vrati hodnotenie pouzivatela z databazy.
    /// </summary>
    /// <returns></returns>
    Rating? GetByPlayer(Player player);

    /// <summary>
    /// Vrati priemerne hodnotenie hry z databazy.
    /// </summary>
    /// <returns></returns>
    double GetAvg();

    /// <summary>
    /// Odstrani vsetky hodnotenia z databazy.
    /// </summary>
    void Reset();
}