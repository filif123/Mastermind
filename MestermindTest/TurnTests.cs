using System;
using System.Diagnostics.CodeAnalysis;
using MastermindCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MestermindTest;

[ExcludeFromCodeCoverage]
[TestClass]
public class TurnTests
{
    [TestMethod]
    public void TurnInit_PegsNull()
    {
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            var _ = new Turn(null!, 3, 2);
        });
    }

    [TestMethod]
    public void TurnInit_InvalidCountOfRightPlaces()
    {
        var pegs = new[] { CodePeg.Blue, CodePeg.Cyan, CodePeg.Green, CodePeg.Red};
        Assert.ThrowsException<ArgumentException>(() =>
        {
            var _ = new Turn(pegs, -1, 2);
        });
    }

    [TestMethod]
    public void TurnInit_InvalidCountOfRightPlaces2()
    {
        var pegs = new[] { CodePeg.Blue, CodePeg.Cyan, CodePeg.Green, CodePeg.Red};
        Assert.ThrowsException<ArgumentException>(() =>
        {
            var _ = new Turn(pegs, 5, 2);
        });
    }

    [TestMethod]
    public void TurnInit_InvalidCountOfRightColors()
    {
        var pegs = new[] { CodePeg.Blue, CodePeg.Cyan, CodePeg.Green, CodePeg.Red};
        Assert.ThrowsException<ArgumentException>(() =>
        {
            var _ = new Turn(pegs, 2, -1);
        });
    }

    [TestMethod]
    public void TurnInit_InvalidCountOfRightColors2()
    {
        var pegs = new[] { CodePeg.Blue, CodePeg.Cyan, CodePeg.Green, CodePeg.Red};
        Assert.ThrowsException<ArgumentException>(() =>
        {
            var _ = new Turn(pegs, 2, 5);
        });
    }

    [TestMethod]
    public void TurnInit_InvalidBothCounts()
    {
        var pegs = new[] { CodePeg.Blue, CodePeg.Cyan, CodePeg.Green, CodePeg.Red};
        Assert.ThrowsException<ArgumentException>(() =>
        {
            var _ = new Turn(pegs, 2, 3);
        });
    }

    [TestMethod]
    public void TurnInit_ValidParams()
    {
        var pegs = new[] { CodePeg.Blue, CodePeg.Cyan, CodePeg.Green, CodePeg.Red};
        var turn = new Turn(pegs, 1, 2);
        Assert.IsTrue(turn.Pegs == pegs && turn.CountOfRightPlaces == 1 && turn.CountOfRightColors == 2);
    }
}