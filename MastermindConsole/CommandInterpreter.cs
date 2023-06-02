using MastermindCore.Entity;
using static System.Console;

namespace MastermindConsole;

public partial class ConsoleUI
{
    private class CommandInterpreter
    {
        private readonly ConsoleUI _ui;

        public CommandInterpreter(ConsoleUI ui)
        {
            _ui = ui;
        }

        public void DoCommands()
        {
            var goBack = false;
            do
            {
                Write(">");
                var cmdText = ReadLine();
                if (cmdText is null)
                {
                    WriteLine("Invalid command.");
                    continue;
                }

                var index = cmdText.IndexOf(' ');
                var cmdType = index == -1 ? cmdText.Trim() : cmdText[..index];
                var args = index == -1 ? "" : cmdText[(index + 1)..];
                switch (cmdType.ToUpper())
                {
                    case "HELP":
                        Help();
                        break;
                    case "BACK":
                    case "EXIT":
                        goBack = true;
                        break;
                    case "RATING":
                        Rating(args);
                        break;
                    case "RATINGS":
                        Ratings();
                        break;
                    case "MSG":
                    case "COMMENT":
                        Comment(args);
                        break;
                    case "COMMENTS":
                        Comments();
                        break;
                    case "SCORES":
                    case "TOP":
                        Scores();
                        break;
                    default:
                        WriteLine("Invalid command");
                        break;
                }

            } while (!goBack);
        }

        private static void Help()
        {
            WriteLine("List of available commands:");
            WriteLine("HELP                 - displays this help menu.");
            WriteLine("BACK | EXIT          - return to the game.");
            WriteLine("RATING <int>         - sets the rating (1-5).");
            WriteLine("RATINGS              - displays the list of all ratings.");
            WriteLine("COMMENT | MSG <text> - creates a comment.");
            WriteLine("COMMENTS             - displays the list of all comments.");
            WriteLine("SCORES | TOP         - displays the list of 3 top players.");
        }

        private void Rating(string args)
        {
            if (!int.TryParse(args, out var num) || num is <= 0 or > 5)
            {
                WriteLine("Invalid rating number. Must between <1,5>.");
                return;
            }

            WriteLine(_ui._ratingService.Add(new Rating {Player = _ui._player, RatedAt = DateTime.Now, Stars = num})
                ? "Your rating was saved. Thank you!"
                : "Your updated rating was saved. Thank you!");
        }

        private void Ratings()
        {
            var ratings = _ui._ratingService.GetAll();
            if (ratings.Count == 0)
            {
                WriteLine("No one has yet rated the game yet.");
                return;
            }

            foreach (var rating in ratings)
            {
                ForegroundColor = rating.Stars switch
                {
                    1 => ConsoleColor.DarkRed,
                    2 => ConsoleColor.Red,
                    3 => ConsoleColor.DarkYellow,
                    4 => ConsoleColor.Yellow,
                    5 => ConsoleColor.Green,
                    _ => ForegroundColor
                };
                WriteLine($"{rating.Player}\t{rating.RatedAt}\t{rating.Stars}/5 stars");
            }
            ResetColor();
        }

        private void Comment(string args)
        {
            if (string.IsNullOrEmpty(args))
            {
                WriteLine("Message cannot be empty.");
                return;
            }
            _ui._commentService.Add(new Comment {Player = _ui._player, CommentedAt = DateTime.Now, Text = args});
            WriteLine("Your comment was saved.");
        }

        private void Comments()
        {
            var comments = _ui._commentService.GetAll();
            if (comments.Count == 0)
            {
                WriteLine("No one has written a comment yet.");
                return;
            }

            foreach (var comment in comments)
            {
                ForegroundColor = ConsoleColor.Cyan;
                WriteLine($"{comment.Player}\t{comment.CommentedAt}");
                ResetColor();
                Write(comment.Text);
                WriteLine();
                WriteLine("------------------------------");
            }
        }

        private void Scores()
        {
            _ui.PrintTopScores();
        }
    }
}