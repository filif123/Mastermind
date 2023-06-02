using System;
using System.Diagnostics.CodeAnalysis;
using MastermindCore.Entity;
using MastermindCore.Service;
using MastermindCore.Service.FileDatabase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MestermindTest.Service.FileDatabase;

[TestClass]
[ExcludeFromCodeCoverage]
public class FileCommentServiceTests
{
    private readonly IPlayerService _players = new FilePlayerService();

    [TestMethod]
    public void Add_Count()
    {
        var service = CreateService();
        var p = AddPlayer("FilipFC1");
        service.Add(new Comment {Player = p, Text = "Test123", CommentedAt = DateTime.Now});

        Assert.AreEqual(1, service.GetAll().Count);
        _players.Reset();
    }

    [TestMethod]
    public void GetAll()
    {
        var service = CreateService();
        var p1 = AddPlayer("JanoFC2");
        var p2 = AddPlayer("PeterFC2");
        service.Add(new Comment {Player = p1, Text = "Good game", CommentedAt = DateTime.Now});
        service.Add(new Comment {Player = p2, Text = "Very bad", CommentedAt = DateTime.Now});

        var all = service.GetAll();

        //aby bol iba 1 assert v jednotke
        Assert.AreEqual((2, "JanoFC2", "Good game", "PeterFC2", "Very bad"), (all.Count, all[0].Player!.Name, all[0].Text, all[1].Player!.Name, all[1].Text));
        _players.Reset();
    }

    [TestMethod]
    public void Reset()
    {
        var service = CreateService();
        var p1 = AddPlayer("JanoFC3");
        var p2 = AddPlayer("PeterFC3");
        service.Add(new Comment {Player = p1, Text = "Good game", CommentedAt = DateTime.Now});
        service.Add(new Comment {Player = p2, Text = "Very bad", CommentedAt = DateTime.Now});

        service.Reset();

        Assert.AreEqual(0, service.GetAll().Count);
        _players.Reset();
    }

    private static ICommentService CreateService()
    {
        var service = new FileCommentService();
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