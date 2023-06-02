using System;
using System.Diagnostics.CodeAnalysis;
using MastermindCore.Entity;
using MastermindCore.Service;
using MastermindCore.Service.SqlDatabase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MestermindTest.Service.SqlDatabase;

[TestClass]
[ExcludeFromCodeCoverage]
public class SqlCommentServiceTests
{
    private readonly IPlayerService _players = new SqlPlayerService();

    [TestMethod]
    public void Add_Count()
    {
        var service = CreateService();
        AddPlayer("FilipSC1");
        service.Add(new Comment {PlayerName= "FilipSC1", Text = "Test123", CommentedAt = DateTime.Now});

        Assert.AreEqual(1, service.GetAll().Count);

        service.Reset();
        _players.Reset();
    }

    [TestMethod]
    public void GetAll()
    {
        var service = CreateService();
        AddPlayer("JanoSC2");
        AddPlayer("PeterSC2");
        service.Add(new Comment {PlayerName = "JanoSC2", Text = "Good game", CommentedAt = DateTime.Now});
        service.Add(new Comment {PlayerName = "PeterSC2", Text = "Very bad", CommentedAt = DateTime.Now});

        var all = service.GetAll();

        //aby bol iba 1 assert v jednotke
        Assert.AreEqual((2, "JanoSC2", "Good game", "PeterSC2", "Very bad"), (all.Count, all[0].PlayerName, all[0].Text, all[1].PlayerName, all[1].Text));

        service.Reset();
        _players.Reset();
    }

    [TestMethod]
    public void Reset()
    {
        var service = CreateService();
        AddPlayer("JanoSC3");
        AddPlayer("PeterSC3");
        service.Add(new Comment {PlayerName = "JanoSC3", Text = "Good game", CommentedAt = DateTime.Now});
        service.Add(new Comment {PlayerName = "PeterSC3", Text = "Very bad", CommentedAt = DateTime.Now});

        service.Reset();

        Assert.AreEqual(0, service.GetAll().Count);
        _players.Reset();
    }

    private static ICommentService CreateService()
    {
        var service = new SqlCommentService();
        service.Reset();
        return service;
    }

    private void AddPlayer(string name)
    {
        var p = new Player(name);
        _players.Add(p);
    }
}