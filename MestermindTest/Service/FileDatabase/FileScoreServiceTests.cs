using System;
using System.Diagnostics.CodeAnalysis;
using MastermindCore.Entity;
using MastermindCore.Service;
using MastermindCore.Service.FileDatabase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MestermindTest.Service.FileDatabase;

[TestClass]
[ExcludeFromCodeCoverage]
public class FileScoreServiceTests
{
    private readonly IPlayerService _players = new FilePlayerService();

    [TestMethod]
    public void Add_CountTopScores()
    {
        var service = CreateService();
        var p = AddPlayer("FilipFS1");
        service.Add(new Score {Player = p, Points = 200, PlayedAt = DateTime.Now});

        Assert.AreEqual(1, service.GetTopThree().Count);
    }

    [TestMethod]
    public void GetTopThree()
    {
        var service = CreateService();
        var p1 = AddPlayer("JanoFS2");
        var p2 = AddPlayer("PeterFS2");
        service.Add(new Score {Player = p1, Points = 100, PlayedAt = DateTime.Now});
        service.Add(new Score {Player = p2, Points = 200, PlayedAt = DateTime.Now});

        var top = service.GetTopThree();

        //aby bol iba 1 assert v jednotke
        Assert.AreEqual((2, "PeterFS2", 200, "JanoFS2", 100), (top.Count, top[0].Player!.Name, top[0].Points, top[1].Player!.Name, top[1].Points));
    }

    [TestMethod]
    public void GetTopThree_CountMorePlayersThanThree()
    {
        var service = CreateService();
        var p1 = AddPlayer("JanoFS3");
        var p2 = AddPlayer("PeterFS3");
        var p3 = AddPlayer("FilipFS3");
        var p4 = AddPlayer("JohnFS3");
        service.Add(new Score {Player = p1, Points = 100, PlayedAt = DateTime.Now});
        service.Add(new Score {Player = p2, Points = 200, PlayedAt = DateTime.Now});
        service.Add(new Score {Player = p3, Points = 50, PlayedAt = DateTime.Now});
        service.Add(new Score {Player = p4, Points = 300, PlayedAt = DateTime.Now});

        var top = service.GetTopThree();

        Assert.AreEqual(3, top.Count);
    }

    [TestMethod]
    public void Reset()
    {
        var service = CreateService();
        var p1 = AddPlayer("JanoFS4");
        var p2 = AddPlayer("PeterFS4");
        service.Add(new Score {Player = p1, Points = 100, PlayedAt = DateTime.Now});
        service.Add(new Score {Player = p2, Points = 80, PlayedAt = DateTime.Now});

        service.Reset();

        Assert.AreEqual(0, service.GetTopThree().Count);
    }

    private static IScoreService CreateService()
    {
        var service = new FileScoreService();
        service.Reset();
        return service;
    }

    private Player AddPlayer(string name)
    {
        var p = new Player(name);
        _players.Add(p);
        return p;
    }
}