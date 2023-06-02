namespace MastermindCore;

/// <summary>
/// Trieda reprezentujuca tah hry.
/// </summary>
public class Turn
{
    /// <summary>
    /// Pole so zadanymi kodmi v danom tahu.
    /// </summary>
    public CodePeg[] Pegs { get; }

    /// <summary>
    /// Vrati pocet spravne rozmiestnenych kodov v danom tahu.
    /// </summary>
    public int CountOfRightPlaces { get; }

    /// <summary>
    /// Vrati pocet spravne najdenych farieb kodov v danom tahu.
    /// Pocet spravne rozmiestnenych kodov nezahrnuje.
    /// </summary>
    public int CountOfRightColors { get; }

    /// <summary>
    /// Vytvori novu instanciu triedy Turn.
    /// </summary>
    /// <param name="pegs">pole kodov</param>
    /// <param name="countOfRightPlaces">pocet spravne rozmiestnenych kodov</param>
    /// <param name="countOfRightColors">pocet spravne najdenych farieb kodov</param>
    /// <exception cref="ArgumentException">Ak pocet kodov nebol v povolenom rozshahu hodnot.</exception>
    public Turn(CodePeg[] pegs, int countOfRightPlaces, int countOfRightColors)
    {
        ArgumentNullException.ThrowIfNull(pegs);
        if (countOfRightColors < 0 || countOfRightColors > pegs.Length)
            throw new ArgumentException($"Parameter {nameof(countOfRightColors)} must be in the range <1, pegs.Length>.");
        if (countOfRightPlaces < 0 || countOfRightPlaces > pegs.Length)
            throw new ArgumentException($"Parameter {nameof(countOfRightPlaces)} must be in the range <1, pegs.Length>.");
        if (countOfRightColors + countOfRightPlaces > pegs.Length)
            throw new ArgumentException($"Sum of values in parameters {nameof(countOfRightPlaces)} and {nameof(countOfRightColors)} must be smaller than pegs.Length ({pegs.Length}).");
        
        Pegs = pegs;
        CountOfRightPlaces = countOfRightPlaces;
        CountOfRightColors = countOfRightColors;
    }
}