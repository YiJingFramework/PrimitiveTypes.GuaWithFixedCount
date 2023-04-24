using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace YiJingFramework.PrimitiveTypes.GuaWithFixedCount.Extensions.Tests;

[TestClass()]
public class GuaAsFixedCountExtensionsTests
{
    [TestMethod()]
    public void AsTest()
    {
        var gua = new Gua(Yinyang.Yin, Yinyang.Yang, Yinyang.Yang);
        var trigram = gua.As<GuaTrigram>();
        Assert.IsTrue(gua.SequenceEqual(trigram));
        Assert.AreEqual(gua, trigram.AsGua());
        _ = Assert.ThrowsException<GuaConversionFailedException>(() => {
            _ = gua.As<GuaHexagram>();
        });
    }

    [TestMethod()]
    public void TryAsTest()
    {
        var gua = new Gua(Yinyang.Yin, Yinyang.Yang, Yinyang.Yang);
        Assert.IsTrue(gua.TryAs<GuaTrigram>(out var trigram));
        Assert.IsTrue(gua.SequenceEqual(trigram));
        Assert.AreEqual(gua, trigram.AsGua());
        Assert.IsFalse(gua.TryAs<GuaHexagram>(out _));
    }
}