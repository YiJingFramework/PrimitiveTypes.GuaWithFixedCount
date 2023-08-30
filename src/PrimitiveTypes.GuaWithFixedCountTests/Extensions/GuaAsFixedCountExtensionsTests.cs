using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace YiJingFramework.PrimitiveTypes.GuaWithFixedCount.Extensions.Tests;

[TestClass()]
public class GuaAsFixedCountExtensionsTests
{
    [TestMethod()]
    public void AsFixedTest()
    {
        var gua = new Gua(Yinyang.Yin, Yinyang.Yang, Yinyang.Yang);
        var trigram = gua.AsFixed<GuaTrigram>();
        Assert.IsTrue(gua.SequenceEqual(trigram));
        Assert.AreEqual(gua, trigram.AsGua());
        _ = Assert.ThrowsException<InvalidCastException>(() =>
        {
            _ = gua.AsFixed<GuaHexagram>();
        });
    }

    [TestMethod()]
    public void TryAsFixedTest()
    {
        var gua = new Gua(Yinyang.Yin, Yinyang.Yang, Yinyang.Yang);
        Assert.IsTrue(gua.TryAsFixed<GuaTrigram>(out var trigram));
        Assert.IsTrue(gua.SequenceEqual(trigram));
        Assert.AreEqual(gua, trigram.AsGua());
        Assert.IsFalse(gua.TryAsFixed<GuaHexagram>(out _));
    }
}