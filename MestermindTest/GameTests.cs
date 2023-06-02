using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MastermindCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MestermindTest;

[ExcludeFromCodeCoverage]
[TestClass]
public class GameTests
{
    [TestMethod]
    public void GameInit_InvalidCodeLength()
    {
        Assert.ThrowsException<ArgumentException>(() =>
        {
            var _ = new Game(-1, 10);
        });
    }

    [TestMethod]
    public void GameInit_InvalidMaxTurns()
    {
        Assert.ThrowsException<ArgumentException>(() =>
        {
            var _ = new Game(4, 21);
        });
    }

    [TestMethod]
    public void GameInit_TestState()
    {
        var game = new Game(4, 10);
        Assert.AreEqual(GameState.NotStarted, game.State);
    }

    [TestMethod]
    public void GameInit_CodeIsNull()
    {
        var game = new Game(4, 10);
        Assert.IsNull(game.CorrectCombination);
    }

    [TestMethod]
    public void StartNew_TestState()
    {
        var game = new Game(4, 10);
        game.StartNew();
        Assert.AreEqual(GameState.Playing, game.State);
    }

    [TestMethod]
    public void GenerateCode_CodeIsNotNull()
    {
        var game = new Game(4, 10);
        game.StartNew();
        Assert.IsNotNull(game.CorrectCombination);
    }

    [TestMethod]
    public void GenerateCode_DoNotAllowDuplicates()
    {
        var game = new Game(4, 10);
        game.StartNew();
        var codepegs = new HashSet<CodePeg>();
        foreach (var peg in game.CorrectCombination!)
        {
            if (codepegs.Contains(peg))
            {
                Assert.Fail();
                return;
            }

            codepegs.Add(peg);
        }
    }

    [TestMethod]
    public void DoMove_Win()
    {
        var game = new Game(4, 10);
        game.StartNew();
        game.DoMove(game.CorrectCombination!);
        Assert.AreEqual(GameState.Solved, game.State);
    }

    [TestMethod]
    public void DoMove_Lose()
    {
        var game = new Game(4, 10);
        game.StartNew();

        var incorrectCombination = game.CorrectCombination!.Reverse().ToArray();
        for (var i = 0; i < game.MaxAllowedTurns; i++) 
            game.DoMove(incorrectCombination);

        Assert.AreEqual(GameState.Lose, game.State);
    }

    [TestMethod]
    public void DoMove_CombinationWrongLength()
    {
        var game = new Game(4, 10);
        var pegs = new[] { CodePeg.Orange, CodePeg.Blue, CodePeg.Violet, CodePeg.White, CodePeg.Green };
        game.StartNew();

        Assert.ThrowsException<ArgumentException>(() =>
        {
            game.DoMove(pegs);
        });
    }

    [TestMethod]
    public void DoMove_BeforeStartNew()
    {
        var game = new Game(4, 10);
        var pegs = new[] { CodePeg.Orange, CodePeg.Blue, CodePeg.Violet, CodePeg.White, CodePeg.Green };

        game.DoMove(pegs);

        Assert.IsTrue(game.State == GameState.NotStarted);
    }

    [TestMethod]
    public void GetScore_GameNotStarted()
    {
        var game = new Game(4, 10);
        Assert.AreEqual(-1, game.GetScore());
    }

    [TestMethod]
    public void GetScore_GameLose()
    {
        var game = new Game(4, 10);
        game.StartNew();

        var incorrectCombination = game.CorrectCombination!.Reverse().ToArray();
        for (var i = 0; i < game.MaxAllowedTurns; i++) 
            game.DoMove(incorrectCombination);

        Assert.AreEqual(0, game.GetScore());
    }

    [TestMethod]
    public void GetScore_GameWinWithFourMaxTurns()
    {
        var game = new Game(4, 10);
        game.StartNew();
        game.DoMove(game.CorrectCombination!);
        var expected = 500 - (int)(game.EndTime - game.StartTime).TotalSeconds + (Game.MaxTurnsCount - game.TurnsCount) * 5;
        Assert.AreEqual(expected, game.GetScore());
    }

    [TestMethod]
    public void GetScore_GameWinAfterWithEightMaxTurns()
    {
        var game = new Game(8, 20);
        game.StartNew();
        game.DoMove(game.CorrectCombination!);
        var expected = 500 - (int)(game.EndTime - game.StartTime).TotalSeconds + (Game.MaxTurnsCount - game.TurnsCount) * 5;
        Assert.AreEqual(expected, game.GetScore());
    }
}