using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using YiJingFramework.PrimitiveTypes.Serialization;

namespace YiJingFramework.PrimitiveTypes.GuaWithFixedCount;

/// <summary>
/// 表示具有固定爻数的卦。
/// Represent a Gua with fixed count of lines.
/// </summary>
/// <typeparam name="TSelf">
/// 类型自己。
/// The type itself.
/// </typeparam>
public interface IGuaWithFixedCount<TSelf> where TSelf : IGuaWithFixedCount<TSelf>
{
    /// <summary>
    /// 转换为 <seealso cref="Gua"/> 。
    /// Convert to a <seealso cref="Gua"/>.
    /// </summary>
    /// <returns>
    /// 卦。
    /// The Gua.
    /// </returns>
    Gua AsGua();

    /// <summary>
    /// 应该具有的爻数。
    /// The expected line count.
    /// </summary>
    static abstract int ExpectedCount { get; }

    /// <summary>
    /// 从 <seealso cref="Gua"/> 转换。
    /// Convert from a <seealso cref="Gua"/>.
    /// </summary>
    /// <param name="gua">
    /// 卦。
    /// The Gua.
    /// </param>
    /// <param name="result">
    /// 结果。
    /// The result.
    /// </param>
    /// <returns>
    /// 指示是否成功。
    /// 除非爻的数量不对，否则总应该成功。
    /// Indicates whether it has succeeded or failed.
    /// It should always be successful unless the line count does not match.
    /// </returns>
    static abstract bool TryFromGua(Gua? gua, [MaybeNullWhen(false)] out TSelf result);
}