using System;
using System.Diagnostics.CodeAnalysis;
using MastermindCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MestermindTest;

[ExcludeFromCodeCoverage]
[TestClass]
public class ExtensionsTests
{
    [TestMethod]
    public void CodePegsToString_ArgNull()
    {
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            Extensions.CodePegsToString(null!);
        });
    }

    [TestMethod]
    public void CodePegsToString_ArgNotNull()
    {
        var pegs = new[] { CodePeg.Blue, CodePeg.Cyan, CodePeg.Green};
        var result = pegs.CodePegsToString();
        Assert.AreEqual("BCG", result);
    }
}