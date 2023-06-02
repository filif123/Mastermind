using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using MastermindCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MestermindTest;

[ExcludeFromCodeCoverage]
[TestClass]
public class CodePegTests
{
    private static CodePeg[] GetAllValues()
    {
        var pegtype = typeof(CodePeg);
        var props = pegtype.GetProperties(BindingFlags.Public | BindingFlags.Static);
        var pegs = new CodePeg[props.Length];
        for (var i = 0; i < props.Length; i++)
        {
            pegs[i] = (CodePeg) props[i].GetValue(null)!;
        }

        return pegs;
    }

    [TestMethod]
    public void GetValues_ContainsAll()
    {
        var expected = GetAllValues();
        var actual = CodePeg.GetValues();
        CollectionAssert.AreEquivalent(expected, actual);
    }

    [TestMethod]
    public void GetValues_ParseCharInvalid()
    {
        var actual = CodePeg.Parse('F');
        Assert.IsNull(actual);
    }

    [TestMethod]
    public void GetValues_ParseCharValid()
    {
        var actual = CodePeg.Parse('B');
        Assert.AreEqual(CodePeg.Blue, actual);
    }

    [TestMethod]
    public void GetValues_ParseCharValidProperties()
    {
        var actual = CodePeg.Parse('B');
        Assert.AreEqual((CodePeg.Blue.Name, CodePeg.Blue.ConsoleColor), (actual!.Name, actual.ConsoleColor));
    }

    [TestMethod]
    public void GetValues_ParseIntInvalid()
    {
        Assert.ThrowsException<ArgumentException>(() =>
        {
            var _ = CodePeg.Parse(-1);
        });
    }

    [TestMethod]
    public void GetValues_ParseIntValid()
    {
        var actual = CodePeg.Parse(1);
        Assert.AreEqual(CodePeg.Red, actual);
    }
}