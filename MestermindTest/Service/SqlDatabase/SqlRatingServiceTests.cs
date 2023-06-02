using System;
using System.Diagnostics.CodeAnalysis;
using MastermindCore.Entity;
using MastermindCore.Service;
using MastermindCore.Service.SqlDatabase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MestermindTest.Service.SqlDatabase;

[TestClass]
[ExcludeFromCodeCoverage]
public class SqlRatingServiceTests
{
    private readonly IPlayerService _players = new SqlPlayerService();

    [TestMethod]
    public void Add_Count()
    {
        var service = CreateService();
        AddPlayer("FilipSR1");
        service.Add(new Rating {PlayerName = "FilipSR1", Stars = 2, RatedAt = DateTime.Now});

        Assert.AreEqual(1, service.GetAll().Count);

        service.Reset();
        _players.Reset();
    }

    [TestMethod]
    public void Add_DuplicatePlayer()
    {
        var service = CreateService();
        AddPlayer("PeterSR2");
        service.Add(new Rating {PlayerName = "PeterSR2", Stars = 5, RatedAt = DateTime.Now});
        var ret = service.Add(new Rating {PlayerName = "PeterSR2", Stars = 3, RatedAt = DateTime.Now});

        var all = service.GetAll();

        Assert.AreEqual((false, 1, "PeterSR2", 3), (ret, all.Count, all[0].PlayerName, all[0].Stars));

        service.Reset();
        _players.Reset();
    }

    [TestMethod]
    public void GetAll()
    {
        var service = CreateService();
        AddPlayer("JanoSR3");
        AddPlayer("PeterSR3");
        service.Add(new Rating {PlayerName = "JanoSR3", Stars = 5, RatedAt = DateTime.Now});
        service.Add(new Rating {PlayerName = "PeterSR3", Stars = 3, RatedAt = DateTime.Now});

        var all = service.GetAll();

        //aby bol iba 1 assert v jednotke
        Assert.AreEqual((2, "JanoSR3", 5, "PeterSR3", 3), (all.Count, all[0].PlayerName, all[0].Stars, all[1].PlayerName, all[1].Stars));

        service.Reset();
        _players.Reset();
    }

    [TestMethod]
    public void GetAvg()
    {
        var service = CreateService();
        AddPlayer("JanoSR4");
        AddPlayer("PeterSR4");
        service.Add(new Rating {PlayerName = "JanoSR4", Stars = 1, RatedAt = DateTime.Now});
        service.Add(new Rating {PlayerName = "PeterSR4", Stars = 5, RatedAt = DateTime.Now});

        var actual = service.GetAvg();

        Assert.AreEqual(3, actual);

        service.Reset();
        _players.Reset();
    }

    [TestMethod]
    public void Reset()
    {
        var service = CreateService();
        AddPlayer("JanoSR5");
        AddPlayer("PeterSR5");
        service.Add(new Rating {PlayerName = "JanoSR5", Stars = 5, RatedAt = DateTime.Now});
        service.Add(new Rating {PlayerName = "PeterSR5", Stars = 3, RatedAt = DateTime.Now});

        service.Reset();

        Assert.AreEqual(0, service.GetAll().Count);
        _players.Reset();
    }

    private static IRatingService CreateService()
    {
        var service = new SqlRatingService();
        service.Reset();
        return service;
    }

    private void AddPlayer(string name)
    {
        var p = new Player(name);
        _players.Add(p);
    }
}