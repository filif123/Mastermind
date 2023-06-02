// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
namespace MastermindCore;

/// <summary>
/// Trieda reprezentujuca hru a hernu plochu.
/// </summary>
public class Game
{
    /// <summary>
    /// Maximalny povoleny pocet kol, ktore moze hrac pri nastaveni hry vybrat.
    /// </summary>
    public const int MaxTurnsCount = 20;

    /// <summary>
    /// Stav hry.
    /// </summary>
    public GameState State { get; set; }

    /// <summary>
    /// Dlzka kodu.
    /// </summary>
    public int CodeLength { get; }

    /// <summary>
    /// Ci je povolena duplicita farieb v kode.
    /// </summary>
    public bool AllowDuplicate { get; }

    /// <summary>
    /// Maximalny povoleny pocet tahov v hre.
    /// </summary>
    public int MaxAllowedTurns { get; }

    /// <summary>
    /// Pocet vykonanych tahov v danej hre.
    /// </summary>
    public int TurnsCount => Turns.Count;

    /// <summary>
    ///    Vrati zoznam tahov hry.
    /// </summary>
    public List<Turn> Turns { get; set; }

    /// <summary>
    /// Vrati spravnu kombináciu (ak pouzivatel prehraje, nech spravny kod vypise na obrazovku).
    /// </summary>
    public CodePeg[]? CorrectCombination { get; set; }

    /// <summary>
    /// Vrati cas zaciatku hry.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Vrati cas ukoncenia hry (prehra alebo vyhra).
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    ///     Vytvori novu instanciu triedy Game.
    /// </summary>
    /// <param name="codeLength">dlzka kodu.</param>
    /// <param name="maxAllowedTurns">maximalny pocet tahov.</param>
    /// <param name="allowDuplicate">povoli duplicitne farby v kode.</param>
    /// <exception cref="ArgumentException">ak parameter maxAllowedTurns nie je v limite 2 az 20.</exception>
    public Game(int codeLength, int maxAllowedTurns, bool allowDuplicate = false)
    {
        if (codeLength is < 1 or > 8)
            throw new ArgumentException("CodeLength must be in range <1,8>.", nameof(codeLength));
        if (maxAllowedTurns is <= 1 or > MaxTurnsCount)
            throw new ArgumentException("MaxAllowedTurns must be in range <2,20>.", nameof(maxAllowedTurns));

        CodeLength = codeLength;
        MaxAllowedTurns = maxAllowedTurns;
        AllowDuplicate = allowDuplicate;
        Turns = new List<Turn>(MaxAllowedTurns);
    }

    /// <summary>
    /// Vytvori (restartuje) novu hru (s nezmenenymi vstupnymi parametrami).
    /// </summary>
    public void StartNew()
    {
        State = GameState.Playing;
        GenerateCode();
        StartTime = DateTime.Now;
        EndTime = DateTime.MinValue;
        Turns.Clear();
    }

    /// <summary>
    /// Restartuje hru bez vytvorenia novej hry.
    /// </summary>
    public void Reset()
    {
        State = GameState.NotStarted;
        Turns.Clear();
    }

    /// <summary>
    /// Vytvori objekt tahu a zanalyzuje pohyb podla kodov, ktore zadal pouzivatel.
    /// </summary>
    /// <param name="codes">vstupne kody zadane pouzivatelom v danom tahu.</param>
    public void DoMove(CodePeg[] codes)
    {
        if (State is GameState.Lose or GameState.NotStarted or GameState.Solved)
            return;

        var turn = AnalyzeTurn(codes);
        Turns.Add(turn);

        if (turn.CountOfRightPlaces == CodeLength)
        {
            State = GameState.Solved;
            EndTime = DateTime.Now;
        }

        else if (TurnsCount == MaxAllowedTurns)
        {
            State = GameState.Lose;
            EndTime = DateTime.Now;
        }
    }

    /// <summary>
    /// Vrati aktualne skore hry.
    /// </summary>
    /// <returns>skore hry</returns>
    public int GetScore()
    {
        if (State == GameState.Lose) return 0;
        if (State == GameState.NotStarted) return -1;

        var score = 500;
        if (State == GameState.Solved)
        {
            score -= (int)(EndTime - StartTime).TotalSeconds;
            score += (MaxTurnsCount - TurnsCount) * 5;
        }
        else
        {
            score -= (int)(DateTime.Now - StartTime).TotalSeconds;
        }

        return Math.Max(score, 0);
    }

    private void GenerateCode()
    {
        CorrectCombination = new CodePeg[CodeLength];
        var used = new List<int>(CodeLength);

        for (var i = 0; i < CodeLength; i++)
        {
            var colorNum = Random.Shared.Next(1, 9);
            if (!AllowDuplicate)
            {
                while (used.Contains(colorNum))
                    colorNum = Random.Shared.Next(1, 9);
            }

            CorrectCombination[i] = CodePeg.Parse(colorNum);
            used.Add(colorNum);
        }
    }

    private Turn AnalyzeTurn(CodePeg[] codes)
    {
        if (codes.Length != CodeLength)
            throw new ArgumentException("Pegs array has invalid length.", nameof(codes));

        var countOfRightPlaces = 0;
        var countOfRightColors = 0;
        var usedPegs = new HashSet<CodePeg>();

        for (var i = 0; i < CodeLength; i++)
        {
            if (codes[i] == CorrectCombination![i] && !usedPegs.Contains(codes[i]))
            {
                countOfRightPlaces++;
                usedPegs.Add(codes[i]);
            }
        }

        for (var i = 0; i < CodeLength; i++)
        {
            if (CorrectCombination!.Contains(codes[i]) && !usedPegs.Contains(codes[i]))
            {
                countOfRightColors++;
                usedPegs.Add(codes[i]);
            }
        }

        return new Turn(codes, countOfRightPlaces, countOfRightColors);
    }
}