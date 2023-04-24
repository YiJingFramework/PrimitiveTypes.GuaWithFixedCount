using Microsoft.VisualStudio.TestTools.UnitTesting;
using YiJingFramework.PrimitiveTypes.GuaWithFixedCount.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiJingFramework.PrimitiveTypes.GuaWithFixedCount.Extensions.Tests;

[TestClass()]
public class GuaAsFixedCountExtensionsTests
{
    [TestMethod()]
    public void AsTest()
    {
        var gua = new Gua(Yinyang.Yin, Yinyang.Yang, Yinyang.Yang);
        var trigram = gua.As<Trigram>();
        Assert.IsTrue(gua.SequenceEqual(trigram));
        Assert.AreEqual(gua, trigram.AsGua());
        Assert.ThrowsException<GuaConversionFailedException>(() => {
            _ = gua.As<Hexagram>();
        });
    }

    [TestMethod()]
    public void TryAsTest()
    {
        var gua = new Gua(Yinyang.Yin, Yinyang.Yang, Yinyang.Yang);
        Assert.IsTrue(gua.TryAs<Trigram>(out var trigram));
        Assert.IsTrue(gua.SequenceEqual(trigram));
        Assert.AreEqual(gua, trigram.AsGua());
        Assert.IsFalse(gua.TryAs<Hexagram>(out _));
    }
}