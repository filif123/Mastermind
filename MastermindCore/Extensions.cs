using System.Text;

namespace MastermindCore;

public static class Extensions
{
    /// <summary>
    /// Vrati textovu reprezentaciu pola kodov podla ich klucov v tvare napr. 'RGBY'.
    /// </summary>
    /// <param name="pegs">pole kodov</param>
    /// <returns>textovy retazec reprezentujuci pole kodov.</returns>
    public static string CodePegsToString(this CodePeg[] pegs)
    {
        ArgumentNullException.ThrowIfNull(pegs);
        var builder = new StringBuilder();
        foreach (var peg in pegs) 
            builder.Append(peg.Key);

        return builder.ToString();
    }
}