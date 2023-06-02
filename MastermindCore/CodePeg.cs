using System.Diagnostics;
using System.Drawing;
using Newtonsoft.Json;

namespace MastermindCore;

/// <summary>
/// Trieda reprezentujuca policko (kamienok) v tahu hry.
/// </summary>
[DebuggerDisplay("Key = {Key}")]
[JsonConverter(typeof(CodePegConverter))]
public class CodePeg
{
    /// <summary>
    /// Cely nazov farby kodu.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Kluc kodu.
    /// </summary>
    public char Key { get; }

    /// <summary>
    /// Ciselny identifikator kodu.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Farba pouzita na zobrazenie v konzolovej aplikacii.
    /// </summary>
    public ConsoleColor ConsoleColor { get; }

    private CodePeg(int id, char key, string name, ConsoleColor consoleColor)
    {
        Id = id;
        Key = key;
        Name = name;
        ConsoleColor = consoleColor;
    }

    public static CodePeg Red { get; } = new(1, 'R', "Red", ConsoleColor.Red);
    public static CodePeg Green { get; } = new(2, 'G', "Green", ConsoleColor.Green);
    public static CodePeg Blue { get; } = new(3, 'B', "Blue", ConsoleColor.Blue);
    public static CodePeg Orange { get; } = new(4, 'O', "Orange", ConsoleColor.DarkYellow);
    public static CodePeg Yellow { get; } = new(5, 'Y', "Yellow", ConsoleColor.Yellow);
    public static CodePeg Cyan { get; } = new(6, 'C', "Cyan", ConsoleColor.Cyan);
    public static CodePeg White { get; } = new(7, 'W', "White", ConsoleColor.White);
    public static CodePeg Violet { get; } = new(8, 'V', "Violet", ConsoleColor.DarkMagenta);

    /// <summary>
    /// Vrati pole vsetkych dostupnych kodov.
    /// </summary>
    /// <returns></returns>
    public static CodePeg[] GetValues() => new[] { Red, Green, Blue, Orange, Yellow, Cyan, White, Violet };

    /// <summary>
    /// Vrati pole vsetkych nazvov dostupnych kodov.
    /// </summary>
    /// <returns></returns>
    public static string[] GetNames() => new[] { "Red", "Green", "Blue", "Orange", "Yellow", "Cyan", "White", "Violet" };

    /// <summary>
    /// Konvertuje identifikator na jej objektovu reprezentaciu. Ak ziadnu nenajde, vyhodi vynimku typu <see cref="ArgumentException"/>.
    /// </summary>
    /// <param name="value">identifikator kodu</param>
    /// <returns>objektovu reprezentacia kodu.</returns>
    /// <exception cref="ArgumentException"></exception>
    public static CodePeg Parse(int value)
    {
        foreach (var codePeg in GetValues())
            if (codePeg.Id == value)
                return codePeg;
        throw new ArgumentException("Invalid index of CodePeg.", nameof(value));
    }

    /// <summary>
    /// Konvertuje kluc kodu na jej objektovu reprezentaciu. Ak ziadnu nenajde, vrati null.
    /// </summary>
    /// <param name="value">kluc kodu</param>
    /// <returns>objektovu reprezentacia kodu.</returns>
    public static CodePeg? Parse(char value) => GetValues().FirstOrDefault(codePeg => codePeg.Key == char.ToUpper(value));

    /// <summary>
    /// Konvertuje nazov kodu na jej objektovu reprezentaciu. Ak ziadnu nenajde, vrati null.
    /// </summary>
    /// <param name="value">nazov kodu</param>
    /// <returns>objektovu reprezentacia kodu.</returns>
    public static CodePeg? Parse(string value) => GetValues().FirstOrDefault(codePeg => codePeg.Name == value.ToUpper());

    /// <inheritdoc />
    public override string ToString() => Name;
}