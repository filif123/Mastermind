using System;
using System.Diagnostics.CodeAnalysis;
using MastermindCore;
using MastermindCore.Entity;
using MastermindCore.Service;
using MastermindCore.Service.FileDatabase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MestermindTest.Service.FileDatabase;

[TestClass]
[ExcludeFromCodeCoverage]
public class FilePlayerServiceTests
{
    [TestMethod]
    public void Add_Count()
    {
        var service = CreateService();
        service.Add(new Player {Name= "FilipSP1", PasswordHash = "123456", RegisteredAt = DateTime.Now});

        Assert.AreEqual(1, service.GetAll().Count);

        service.Reset();
    }

    [TestMethod]
    public void Get()
    {
        var service = CreateService();
        service.Add(new Player {Name= "FilipSP2", PasswordHash = Encryptor.Md5Hash("123456"), RegisteredAt = DateTime.Now});

        Assert.IsNotNull(service.Get("FilipSP2","123456")!.Name);

        service.Reset();
    }

    [TestMethod]
    public void Get_NotFound()
    {
        var service = CreateService();
        service.Add(new Player {Name= "FilipSP3", PasswordHash = "123456", RegisteredAt = DateTime.Now});

        Assert.AreEqual(null, service.Get("janko","heslo"));

        service.Reset();
    }

    [TestMethod]
    public void Exists()
    {
        var service = CreateService();
        service.Add(new Player {Name= "FilipSP4", PasswordHash = "123456", RegisteredAt = DateTime.Now});

        Assert.AreEqual(true, service.Exists("FilipSP4"));

        service.Reset();
    }

    [TestMethod]
    public void Exists_NotFound()
    {
        var service = CreateService();
        service.Add(new Player {Name= "FilipSP5", PasswordHash = "123456", RegisteredAt = DateTime.Now});

        Assert.AreEqual(false, service.Exists("jano"));

        service.Reset();
    }

    [TestMethod]
    public void GetAll()
    {
        var service = CreateService();
        service.Add(new Player {Name = "JanoSP6", PasswordHash = "123456", RegisteredAt = DateTime.Now});
        service.Add(new Player {Name = "PeterSP6", PasswordHash = "password", RegisteredAt = DateTime.Now});

        var all = service.GetAll();

        //aby bol iba 1 assert v jednotke
        Assert.AreEqual((2, "JanoSP6", "123456", "PeterSP6", "password"), (all.Count, all[0].Name, all[0].PasswordHash, all[1].Name, all[1].PasswordHash));

        service.Reset();
    }

    [TestMethod]
    public void Reset()
    {
        var service = CreateService();
        service.Add(new Player {Name = "JanoSP7", PasswordHash = "123456", RegisteredAt = DateTime.Now});
        service.Add(new Player {Name = "PeterSP7", PasswordHash = "password", RegisteredAt = DateTime.Now});

        service.Reset();

        Assert.AreEqual(0, service.GetAll().Count);
    }

    private static IPlayerService CreateService()
    {
        var service = new FilePlayerService();
        service.Reset();
        return service;
    }
}