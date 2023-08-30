using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace YiJingFramework.PrimitiveTypes.GuaWithFixedCount.Tests;

[TestClass()]
public class GuaTrigramTests
{
    [TestMethod()]
    public void GuaTrigramTest()
    {
        var trigram1 = new GuaTrigram(Yinyang.Yang, Yinyang.Yin, Yinyang.Yang);
        var trigram2 = new GuaTrigram(new[] { Yinyang.Yang, Yinyang.Yin, Yinyang.Yang });

        Assert.AreEqual(trigram1, trigram2);

        _ = Assert.ThrowsException<ArgumentException>(() =>
        {
            _ = new GuaTrigram(new[] { Yinyang.Yang, Yinyang.Yin, Yinyang.Yang, Yinyang.Yin });
        });
    }

    [TestMethod()]
    public void GetEnumeratorTest()
    {
        var random = new Random();
        for (int i = 0; i < 100; i++)
        {
            var yaos = Enumerable.Repeat(0, 3)
                .Select(_ => random.Next(0, 2))
                .Select(i => (Yinyang)i)
                .ToArray();
            var gua = new GuaTrigram(yaos);
            Assert.IsTrue(gua.SequenceEqual(yaos));
        }
    }

    [TestMethod()]
    public void CompareToTest()
    {
        var random = new Random();
        bool noEqualTested = true;
        for (int i = 0; i < 10000 || noEqualTested; i++)
        {
            var yaos1 = Enumerable.Repeat(0, 3)
                .Select(_ => random.Next(0, 2))
                .Select(i => (Yinyang)i)
                .ToArray();
            var yaos2 = Enumerable.Repeat(0, 3)
                .Select(_ => random.Next(0, 2))
                .Select(i => (Yinyang)i)
                .ToArray();
            var trigram1 = new GuaTrigram(yaos1);
            var trigram2 = new GuaTrigram(yaos2);
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
            var yaos = Enumerable.Repeat(0, 3)
                .Select(_ => random.Next(0, 2))
                .Select(i => (Yinyang)i)
                .ToArray();
            var gua1 = new GuaTrigram(yaos);
            var gua2 = new Gua(yaos);
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
            var yaos1 = Enumerable.Repeat(0, 3)
                .Select(_ => random.Next(0, 2))
                .Select(i => (Yinyang)i)
                .ToArray();
            var yaos2 = Enumerable.Repeat(0, 3)
                .Select(_ => random.Next(0, 2))
                .Select(i => (Yinyang)i)
                .ToArray();
            var trigram1 = new GuaTrigram(yaos1);
            var trigram2 = new GuaTrigram(yaos2);
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
            var yaos = Enumerable.Repeat(0, 3)
                .Select(_ => random.Next(0, 2))
                .Select(i => (Yinyang)i)
                .ToArray();
            var gua1 = new GuaTrigram(yaos);
            var gua2 = new Gua(yaos);
            Assert.AreEqual(gua1.ToString(), gua2.ToString());
        }
    }

    [TestMethod()]
    public void ParseTest()
    {
        var random = new Random();
        for (int i = 0; i < 100; i++)
        {
            var yaos = Enumerable.Repeat(0, 3)
                .Select(_ => random.Next(0, 2))
                .Select(i => (Yinyang)i)
                .ToArray();
            var gua1 = new GuaTrigram(yaos);
            var gua2 = GuaTrigram.Parse(gua1.ToString());
            Assert.AreEqual(gua1, gua2);
        }
    }

    [TestMethod()]
    public void TryParseTest()
    {
        var random = new Random();
        for (int i = 0; i < 100; i++)
        {
            var yaos = Enumerable.Repeat(0, 3)
                .Select(_ => random.Next(0, 2))
                .Select(i => (Yinyang)i)
                .ToArray();
            var gua1 = new GuaTrigram(yaos);
            Assert.IsTrue(GuaTrigram.TryParse(gua1.ToString(), out var gua2));
            Assert.AreEqual(gua1, gua2);
        }
    }

    [TestMethod()]
    public void AsGuaTest()
    {
        var random = new Random();
        for (int i = 0; i < 100; i++)
        {
            var yaos = Enumerable.Repeat(0, 3)
                .Select(_ => random.Next(0, 2))
                .Select(i => (Yinyang)i)
                .ToArray();
            var gua1 = new GuaTrigram(yaos);
            var gua2 = new Gua(yaos);
            Assert.AreEqual(gua2, gua1.AsGua());
        }
    }

    [TestMethod()]
    public void CalculatingTest()
    {
        Assert.AreEqual(GuaEmpty.Parse(""), GuaEmpty.Parse("") & GuaEmpty.Parse(""));
        Assert.AreEqual(GuaWith1Yao.Parse("1"), GuaWith1Yao.Parse("1") & GuaWith1Yao.Parse("1"));
        Assert.AreEqual(GuaWith4Yaos.Parse("1000"), GuaWith4Yaos.Parse("1100") & GuaWith4Yaos.Parse("1010"));

        Assert.AreEqual(GuaEmpty.Parse(""), GuaEmpty.Parse("") | GuaEmpty.Parse(""));
        Assert.AreEqual(GuaWith1Yao.Parse("1"), GuaWith1Yao.Parse("1") | GuaWith1Yao.Parse("1"));
        Assert.AreEqual(GuaWith4Yaos.Parse("1110"), GuaWith4Yaos.Parse("1100") | GuaWith4Yaos.Parse("1010"));

        Assert.AreEqual(GuaEmpty.Parse(""), GuaEmpty.Parse("") ^ GuaEmpty.Parse(""));
        Assert.AreEqual(GuaWith1Yao.Parse("0"), GuaWith1Yao.Parse("1") ^ GuaWith1Yao.Parse("1"));
        Assert.AreEqual(GuaWith4Yaos.Parse("0110"), GuaWith4Yaos.Parse("1100") ^ GuaWith4Yaos.Parse("1010"));

        Assert.AreEqual(GuaEmpty.Parse(""), ~GuaEmpty.Parse(""));
        Assert.AreEqual(GuaWith1Yao.Parse("0"), ~GuaWith1Yao.Parse("1"));
        Assert.AreEqual(GuaWith2Yaos.Parse("01"), ~GuaWith2Yaos.Parse("10"));
    }
}