using System;
using System.Diagnostics.CodeAnalysis;
using MastermindCore.Entity;
using MastermindCore.Service;
using MastermindCore.Service.SqlDatabase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MestermindTest.Service.SqlDatabase;

[TestClass]
[ExcludeFromCodeCoverage]
public class SqlScoreServiceTests
{
    private readonly IPlayerService _players = new SqlPlayerService();

    [TestMethod]
    public void Add_CountTopScores()
    {
        var service = CreateService();
        AddPlayer("FilipSS1");
        service.Add(new Score {PlayerName = "FilipSS1", Points = 200, PlayedAt = DateTime.Now});

        Assert.AreEqual(1, service.GetTopThree().Count);

        service.Reset();
        _players.Reset();
    }

    [TestMethod]
    public void GetTopThree()
    {
        var service = CreateService();
        AddPlayer("JanoSS3");
        AddPlayer("PeterSS3");
        service.Add(new Score {PlayerName = "JanoSS3", Points = 100, PlayedAt = DateTime.Now});
        service.Add(new Score {PlayerName = "PeterSS3", Points = 200, PlayedAt = DateTime.Now});

        var top = service.GetTopThree();

        //aby bol iba 1 assert v jednotke
        Assert.AreEqual((2, "PeterSS3", 200, "JanoSS3", 100), (top.Count, top[0].PlayerName, top[0].Points, top[1].PlayerName, top[1].Points));

        service.Reset();
        _players.Reset();
    }

    [TestMethod]
    public void GetTopThree_CountMorePlayersThanThree()
    {
        var service = CreateService();
        AddPlayer("JanoSS4");
        AddPlayer("PeterSS4");
        AddPlayer("FilipSS4");
        AddPlayer("JohnSS4");
        service.Add(new Score {PlayerName = "JanoSS4", Points = 100, PlayedAt = DateTime.Now});
        service.Add(new Score {PlayerName = "PeterSS4", Points = 200, PlayedAt = DateTime.Now});
        service.Add(new Score {PlayerName = "FilipSS4", Points = 50, PlayedAt = DateTime.Now});
        service.Add(new Score {PlayerName = "JohnSS4", Points = 300, PlayedAt = DateTime.Now});

        var top = service.GetTopThree();

        Assert.AreEqual(3, top.Count);

        service.Reset();
        _players.Reset();
    }

    [TestMethod]
    public void Reset()
    {
        var service = CreateService();
        AddPlayer("JanoSS5");
        AddPlayer("PeterSS5");
        service.Add(new Score {PlayerName = "JanoSS5", Points = 100, PlayedAt = DateTime.Now});
        service.Add(new Score {PlayerName = "PeterSS5", Points = 80, PlayedAt = DateTime.Now});

        service.Reset();

        Assert.AreEqual(0, service.GetTopThree().Count);
        _players.Reset();
    }

    private static IScoreService CreateService()
    {
        var service = new SqlScoreService();
        service.Reset();
        return service;
    }

    private void AddPlayer(string name)
    {
        var p = new Player(name);
        _players.Add(p);
    }
}