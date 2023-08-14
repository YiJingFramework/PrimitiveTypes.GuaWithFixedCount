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
public interface IGuaWithFixedCount<TSelf> :
    IReadOnlyList<Yinyang>, IComparable<TSelf>, IEquatable<TSelf>,
    IParsable<TSelf>, IEqualityOperators<TSelf, TSelf, bool>,
    IStringConvertibleForJson<TSelf>,
    IBitwiseOperators<TSelf, TSelf, TSelf>
    where TSelf : IGuaWithFixedCount<TSelf>
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

    /// <summary>
    /// 从字符串转回。
    /// Convert from a string.
    /// </summary>
    /// <param name="s">
    /// 可以表示此卦的字符串。
    /// The string that represents the Gua.
    /// </param>
    /// <returns>
    /// 卦。
    /// The Gua.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="s"/> 是 <c>null</c> 。
    /// <paramref name="s"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="FormatException">
    /// 传入字符串的格式不受支持。
    /// The input string was not in the supported format.
    /// </exception>
    static abstract TSelf Parse(string s);

    /// <summary>
    /// 从字符串转回。
    /// Convert from a string.
    /// </summary>
    /// <param name="s">
    /// 可以表示此卦的字符串。
    /// The string that represents the Gua.
    /// </param>
    /// <param name="result">
    /// 卦。
    /// The Gua.
    /// </param>
    /// <returns>
    /// 一个指示转换成功与否的值。
    /// A value indicates whether it has been successfully converted or not.
    /// </returns>
    static abstract bool TryParse(
        [NotNullWhen(true)] string? s,
        [MaybeNullWhen(false)] out TSelf result);
}