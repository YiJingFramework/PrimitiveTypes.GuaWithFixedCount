using YiJingFramework.PrimitiveTypes.GuaWithFixedCount;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YiJingFramework.PrimitiveTypes.GuaWithFixedCount.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiJingFramework.PrimitiveTypes.GuaWithFixedCount.Tests;

[TestClass()]
public class TrigramTests
{
    [TestMethod()]
    public void TrigramTest()
    {
        var trigram1 = new Trigram(Yinyang.Yang, Yinyang.Yin, Yinyang.Yang);
        var trigram2 = new Trigram(new[] { Yinyang.Yang, Yinyang.Yin, Yinyang.Yang });
        var trigram3 = new Trigram(new[] { Yinyang.Yang, Yinyang.Yin, Yinyang.Yang }.AsEnumerable());

        Assert.AreEqual(trigram1, trigram2);
        Assert.AreEqual(trigram2, trigram3);
        Assert.AreEqual(trigram1, trigram3);

        _ = Assert.ThrowsException<ArgumentException>(() => {
            new Trigram(Yinyang.Yang, Yinyang.Yin, Yinyang.Yang, Yinyang.Yin);
        });
        _ = Assert.ThrowsException<ArgumentException>(() => {
            new Trigram(new[] { Yinyang.Yang, Yinyang.Yin, Yinyang.Yang, Yinyang.Yin }.AsEnumerable());
        });
    }

    [TestMethod()]
    public void GetEnumeratorTest()
    {
        var random = new Random();
        for (int i = 0; i < 100; i++)
        {
            var lines = Enumerable.Repeat(0, 3)
                .Select(_ => random.Next(0, 2))
                .Select(i => (Yinyang)i)
                .ToArray();
            var gua = new Trigram(lines);
            Assert.IsTrue(gua.SequenceEqual(lines));
        }
    }

    [TestMethod()]
    public void CompareToTest()
    {
        var random = new Random();
        bool noEqualTested = true;
        for (int i = 0; i < 10000 || noEqualTested; i++)
        {
            var lines1 = Enumerable.Repeat(0, 3)
                .Select(_ => random.Next(0, 2))
                .Select(i => (Yinyang)i)
                .ToArray();
            var lines2 = Enumerable.Repeat(0, 3)
                .Select(_ => random.Next(0, 2))
                .Select(i => (Yinyang)i)
                .ToArray();
            var trigram1 = new Trigram(lines1);
            var trigram2 = new Trigram(lines2);
            Assert.AreEqual(
                trigram1.AsGua().CompareTo(trigram2.AsGua()),
                trigram1.CompareTo(trigram2));
            if (trigram1.CompareTo(trigram2) == 0)
                noEqualTested = false;
        }
    }

    [TestMethod()]
    public void GetHashCodeTest()
    {
        var random = new Random();
        for (int i = 0; i < 100; i++)
        {
            var lines = Enumerable.Repeat(0, 3)
                .Select(_ => random.Next(0, 2))
                .Select(i => (Yinyang)i)
                .ToArray();
            var gua1 = new Trigram(lines);
            var gua2 = new Gua(lines);
            Assert.AreEqual(gua1.GetHashCode(), gua2.GetHashCode());
        }
    }

    [TestMethod()]
    public void EqualsTest()
    {
        var random = new Random();
        bool noEqualTested = true;
        for (int i = 0; i < 10000 || noEqualTested; i++)
        {
            var lines1 = Enumerable.Repeat(0, 3)
                .Select(_ => random.Next(0, 2))
                .Select(i => (Yinyang)i)
                .ToArray();
            var lines2 = Enumerable.Repeat(0, 3)
                .Select(_ => random.Next(0, 2))
                .Select(i => (Yinyang)i)
                .ToArray();
            var trigram1 = new Trigram(lines1);
            var trigram2 = new Trigram(lines2);
            Assert.AreEqual(
                trigram1.AsGua().Equals(trigram2.AsGua()),
                trigram1.Equals(trigram2));
            if (trigram1.Equals(trigram2))
                noEqualTested = false;
        }
    }

    [TestMethod()]
    public void ToStringTest()
    {
        var random = new Random();
        for (int i = 0; i < 100; i++)
        {
            var lines = Enumerable.Repeat(0, 3)
                .Select(_ => random.Next(0, 2))
                .Select(i => (Yinyang)i)
                .ToArray();
            var gua1 = new Trigram(lines);
            var gua2 = new Gua(lines);
            Assert.AreEqual(gua1.ToString(), gua2.ToString());
        }
    }

    [TestMethod()]
    public void ParseTest()
    {
        var random = new Random();
        for (int i = 0; i < 100; i++)
        {
            var lines = Enumerable.Repeat(0, 3)
                .Select(_ => random.Next(0, 2))
                .Select(i => (Yinyang)i)
                .ToArray();
            var gua1 = new Trigram(lines);
            var gua2 = Trigram.Parse(gua1.ToString());
            Assert.AreEqual(gua1, gua2);
        }
    }

    [TestMethod()]
    public void TryParseTest()
    {
        var random = new Random();
        for (int i = 0; i < 100; i++)
        {
            var lines = Enumerable.Repeat(0, 3)
                .Select(_ => random.Next(0, 2))
                .Select(i => (Yinyang)i)
                .ToArray();
            var gua1 = new Trigram(lines);
            Assert.IsTrue(Trigram.TryParse(gua1.ToString(), out var gua2));
            Assert.AreEqual(gua1, gua2);
        }
    }

    [TestMethod()]
    public void AsGuaTest()
    {
        var random = new Random();
        for (int i = 0; i < 100; i++)
        {
            var lines = Enumerable.Repeat(0, 3)
                .Select(_ => random.Next(0, 2))
                .Select(i => (Yinyang)i)
                .ToArray();
            var gua1 = new Trigram(lines);
            var gua2 = new Gua(lines);
            Assert.AreEqual(gua2, gua1.AsGua());
        }
    }
}