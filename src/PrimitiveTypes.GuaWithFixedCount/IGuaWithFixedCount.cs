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
    IStringConvertibleForJson<TSelf>
    where TSelf : IGuaWithFixedCount<TSelf>
{
    /// <summary>
    /// 转换为 <seealso cref="Gua"/> 。
    /// Convert to <seealso cref="Gua"/>.
    /// </summary>
    /// <returns>
    /// 卦。
    /// A Gua.
    /// </returns>
    Gua AsGua();

    /// <summary>
    /// 从爻的集合创建。
    /// Create from a collection of lines.
    /// </summary>
    /// <param name="lines">
    /// 爻。
    /// The lines.
    /// </param>
    /// <param name="result">
    /// 创建结果。
    /// Creation result.
    /// </param>
    /// <param name="message">
    /// 创建失败时提供的消息。
    /// Message provided when creation failed.
    /// </param>
    /// <returns>
    /// 一个指示创建成功与否的值。
    /// A value indicates whether it has been successfully created or not.
    /// </returns>
    static abstract bool TryFromLines(
        IEnumerable<Yinyang> lines,
        [MaybeNullWhen(false)] out TSelf result,
        [MaybeNullWhen(true)] out string message);
}