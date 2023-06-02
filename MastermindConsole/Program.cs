using MastermindConsole;
using MastermindCore;
using MastermindCore.Entity;
using static System.Console;

// Entry point of the application

Title = "Mastermind";
ConsoleUI.PrintGameName();

var validCodeLen = false;
var codeLen = 0;
while (!validCodeLen)
{
    WriteLine("Enter code length (number 4, 6 or 8) (default = 4):");
    var codeLenStr = ReadLine();
    if (codeLenStr == "")
    {
        validCodeLen = true;
        codeLen = 4;
    }
    else if (int.TryParse(codeLenStr, out codeLen) && codeLen is 4 or 6 or 8) 
        validCodeLen = true;
    else
        WriteLine("Invalid input!");
}

var validMaxTurns = false;
var maxTurns = 0;
while (!validMaxTurns)
{
    WriteLine("Enter max turns (number <2,20>) (default = 10):");
    var codeLenStr = ReadLine();
    if (codeLenStr == "")
    {
        validMaxTurns = true;
        maxTurns = 10;
    }
    else if (int.TryParse(codeLenStr, out maxTurns) && maxTurns is >1 and <21)
        validMaxTurns = true;
    else
        WriteLine("Invalid input!");
}

var allowDuplicates = false;
WriteLine("Allow duplicates: ('y' or 'Y' if YES, any other if NO):");
var duplicatesStr = ReadLine();
if (duplicatesStr is "y" or "Y" or "yes" or "YES") 
    allowDuplicates = true;

var game = new Game(codeLen, maxTurns, allowDuplicates);
var ui = new ConsoleUI(game, new Player(Environment.UserName));
ui.Run();