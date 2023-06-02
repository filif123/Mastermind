using System;
using System.Diagnostics.CodeAnalysis;
using MastermindCore.Entity;
using MastermindCore.Service;
using MastermindCore.Service.FileDatabase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MestermindTest.Service.FileDatabase;

[TestClass]
[ExcludeFromCodeCoverage]
public class FileRatingServiceTests
{
    private readonly IPlayerService _players = new FilePlayerService();

    [TestMethod]
    public void Add_Count()
    {
        var service = CreateService();
        var p = AddPlayer("FilipFR1");
        service.Add(new Rating {Player = p, Stars = 2, RatedAt = DateTime.Now});

        Assert.AreEqual(1, service.GetAll().Count);
        _players.Reset();
    }

    [TestMethod]
    public void Add_IncorrectStars()
    {
        var service = CreateService();
        var p = AddPlayer("FilipFR2");

        Assert.ThrowsException<ArgumentException>(() => service.Add(new Rating {Player = p, Stars = -1, RatedAt = DateTime.Now}));
        _players.Reset();
    }

    [TestMethod]
    public void Add_DuplicatePlayer()
    {
        var service = CreateService();
        var p = AddPlayer("PeterFR3");
        service.Add(new Rating {Player = p, Stars = 5, RatedAt = DateTime.Now});
        var ret = service.Add(new Rating {Player = p, Stars = 3, RatedAt = DateTime.Now});

        var all = service.GetAll();

        Assert.AreEqual((false, 1, "PeterFR3", 3), (ret, all.Count, all[0].Player!.Name, all[0].Stars));
        _players.Reset();
    }

    [TestMethod]
    public void GetAll()
    {
        var service = CreateService();
        var p1 = AddPlayer("JanoFR4");
        var p2 = AddPlayer("PeterFR4");
        service.Add(new Rating {Player = p1, Stars = 5, RatedAt = DateTime.Now});
        service.Add(new Rating {Player = p2, Stars = 3, RatedAt = DateTime.Now});

        var all = service.GetAll();

        //aby bol iba 1 assert v jednotke
        Assert.AreEqual((2, "JanoFR4", 5, "PeterFR4", 3), (all.Count, all[0].Player!.Name, all[0].Stars, all[1].Player!.Name, all[1].Stars));
        _players.Reset();
    }

    [TestMethod]
    public void GetAvg()
    {
        var service = CreateService();
        var p1 = AddPlayer("JanoFR5");
        var p2 = AddPlayer("PeterFR5");
        service.Add(new Rating {Player = p1, Stars = 1, RatedAt = DateTime.Now});
        service.Add(new Rating {Player = p2, Stars = 5, RatedAt = DateTime.Now});

        var actual = service.GetAvg();

        Assert.AreEqual(3, actual);

        service.Reset();
        _players.Reset();
    }

    [TestMethod]
    public void Reset()
    {
        var service = CreateService();
        var p1 = AddPlayer("JanoFR6");
        var p2 = AddPlayer("PeterFR6");
        service.Add(new Rating {Player = p1, Stars = 5, RatedAt = DateTime.Now});
        service.Add(new Rating {Player = p2, Stars = 3, RatedAt = DateTime.Now});

        service.Reset();

        Assert.AreEqual(0, service.GetAll().Count);
        _players.Reset();
    }

    private static IRatingService CreateService()
    {
        var service = new FileRatingService();
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