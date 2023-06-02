using MastermindCore;
using MastermindCore.Entity;
using MastermindCore.Service;
using MastermindCore.Service.FileDatabase;
using static System.Console;

namespace MastermindConsole;

public partial class ConsoleUI
{
    private readonly Game _game;
    private readonly Player _player;

    private readonly IScoreService _scoreService = new FileScoreService();
    private readonly ICommentService _commentService = new FileCommentService();
    private readonly IRatingService _ratingService = new FileRatingService();
    private readonly CommandInterpreter _commandInterpreter;

    public ConsoleUI(Game game, Player player)
    {
        _game = game;
        _player = player;
        _commandInterpreter = new CommandInterpreter(this);
    }

    public void Run()
    {
        var runAgain = false;
        do
        {
            _game.StartNew();
            do
            {
                Print();
                ProcessInput();
            } 
            while (_game.State == GameState.Playing);

            Print();

            if (_game.State == GameState.Solved)
            {
                ForegroundColor = ConsoleColor.Green;
                WriteLine($"YOU WON on the {_game.TurnsCount}. attempt !!! - Good game.");
                _scoreService.Add(new Score {Player = _player, Points = _game.GetScore(), PlayedAt = DateTime.Now});
            }
            else
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine($"YOU LOSE !!! - Code was '{_game.CorrectCombination!.CodePegsToString()}'.");
            }

            WriteLine();
            ResetColor();
            PrintTopScores();
            WriteLine();

            WriteLine("Do you want to play again? ('y' or 'Y' if YES, any other if NO) or type 'cmd':");
            var runAgainStr = ReadLine();
            if (runAgainStr is null or "y" or "yes" or "Y" or "YES")
            {
                runAgain = true;
            }
            else if(runAgainStr.ToUpper() == "CMD")
            {
                _commandInterpreter.DoCommands();
                runAgain = false;
            }
        } while (runAgain);
    }

    private void Print()
    {
        Clear();
        PrintGameName();
        PrintScore();
        PrintField();
        PrintAvailableColors();
        WriteLine();
        WriteLine("Enter codes or type 'cmd':");
    }

    //Rekurzivna funkcia - vola sa viackrat v pripade zleho vstupu.
    public void ProcessInput()
    {
        var input = ReadLine();
        if (input == null)
            return;

        input = input.Trim();

        if (input.ToUpper() == "CMD")
        {
            Clear();
            _commandInterpreter.DoCommands();
            Print();
            return;
        }

        if (input.Length != _game.CodeLength)
        {
            WriteLine("Invalid input.");
            ProcessInput();
            return;
        }

        var codes = new CodePeg[_game.CodeLength];
        for (var i = 0; i < input.Length; i++)
        {
            var peg = CodePeg.Parse(input[i]);
            if (peg == null)
            {
                WriteLine($"Invalid input on index {i} - Invalid char '{input[i]}'.");
                ProcessInput();
                return;
            }
            
            codes[i] = peg;
        }

        _game.DoMove(codes);
    }

    public static void PrintGameName()
    {
        WriteWithColor(" M ", ConsoleColor.DarkRed);
        WriteWithColor(" A ", ConsoleColor.DarkBlue);
        WriteWithColor(" S ", ConsoleColor.Yellow, ConsoleColor.Black);
        WriteWithColor(" T ", ConsoleColor.DarkGreen);
        WriteWithColor(" E ", ConsoleColor.Gray, ConsoleColor.Black);
        WriteWithColor(" R ", ConsoleColor.DarkMagenta);
        WriteWithColor(" M ");
        WriteWithColor(" I ", ConsoleColor.DarkYellow);
        WriteWithColor(" N ", ConsoleColor.Cyan, ConsoleColor.Black);
        WriteWithColor(" D ", ConsoleColor.Green, ConsoleColor.Black);
        WriteLine();
    }

    private static void WriteWithColor(string text, 
        ConsoleColor back = ConsoleColor.Black, 
        ConsoleColor fore = ConsoleColor.White)
    {
        BackgroundColor = back;
        ForegroundColor = fore;
        Write(text);
        ResetColor();
    }

    private void PrintScore()
    {
        ForegroundColor = ConsoleColor.Green;
        WriteLine("Score: " + _game.GetScore());
        ResetColor();
    }

    private static void PrintAvailableColors()
    {
        WriteLine("Available colors:");
        foreach (var peg in CodePeg.GetValues())
        {
            ForegroundColor = peg.ConsoleColor;
            Write($"{peg.Name} - '{peg.Key}' ");
        }

        ResetColor();
    }

    private void PrintField()
    {
        //horny kraj
        for (var i = 0; i < _game.CodeLength; i++) 
            Write("+---");
        WriteLine('+');

        for (var r = 0; r < _game.MaxAllowedTurns * 2; r++)
        {
            for (var c = 0; c < _game.CodeLength; c++)
            {
                switch (r % 2)
                {
                    case 0 when r/2 < _game.TurnsCount: //vykreslujem vnutro herneho pola - farebne kody (uz vyplnene)
                    {
                        Write('|');
                        var peg = _game.Turns[r / 2].Pegs[c];
                        BackgroundColor = peg.ConsoleColor;
                        ForegroundColor = ConsoleColor.Black;
                        Write($" {peg.Key} ");
                        ResetColor();
                        break;
                    }
                    case 0: // vykreslujem vnutro herneho pola (este ked neboli vyplnene)
                        Write("|   ");
                        break;
                    default: // vykreslujem horizontalne steny
                        Write("+---");
                        break;
                }
            }

            if (r % 2 == 0)
            {
                Write("| ");
                if (r/2 < _game.TurnsCount)
                {
                    var turn = _game.Turns[r / 2];
                    for (var i = 0; i < turn.CountOfRightPlaces; i++)
                    {
                        ForegroundColor = ConsoleColor.Red;
                        Write('X');
                    }

                    for (var i = 0; i < turn.CountOfRightColors; i++)
                    {
                        ForegroundColor = ConsoleColor.DarkCyan;
                        Write('X');
                    }

                    ResetColor();
                }
                WriteLine();
            }
            else
                WriteLine('+');
        }
    }

    private void PrintTopScores()
    {
        ForegroundColor = ConsoleColor.Yellow;
        WriteLine("+------------------------+");
        WriteLine("|       TOP SCORES       |");
        WriteLine("+------------------------+");
        ResetColor();

        var scores = _scoreService.GetTopThree();
        for (var i = 0; i < scores.Count; i++)
        {
            ForegroundColor = i switch
            {
                0 => ConsoleColor.DarkYellow,
                1 => ConsoleColor.DarkGray,
                2 => ConsoleColor.Red,
                _ => ForegroundColor
            };
            WriteLine("{0}. {1} - {2} ({3})", i + 1, scores[i].Player, scores[i].Points, scores[i].PlayedAt);
        }

        ResetColor();
        WriteLine();
    }
}