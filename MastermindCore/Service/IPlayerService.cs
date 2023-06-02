using System.Dynamic;
using MastermindCore.Entity;

namespace MastermindCore.Service;

public interface IPlayerService
{
    /// <summary>
    /// Prida hraca do databazy.
    /// </summary>
    /// <param name="player">hrac</param>
    void Add(Player player);

    /// <summary>
    /// Vrati instanciu hraca podla zadaneho mena a hesla.
    /// </summary>
    /// <returns>hrac</returns>
    Player? Get(string name, string password);

    /// <summary>
    /// Zisti ci sa v databaze nachadza ucet s danym prihlasovacim menom/
    /// </summary>
    /// <param name="name">prihlasovacie meno hraca</param>
    /// <returns>ci sa v databaze nachadza alebo nenachadza uset s tymto prihlasovacim menom</returns>
    bool Exists(string name);

    /// <summary>
    /// Vrati vsetkych hracov z databazy.
    /// </summary>
    /// <returns></returns>
    IList<Player> GetAll();

    /// <summary>
    /// Odstrani vsetkych hracov z databazy.
    /// </summary>
    void Reset();
}