using System.Diagnostics.CodeAnalysis;

namespace YiJingFramework.PrimitiveTypes.GuaWithFixedCount.Extensions;

/// <summary>
/// 表示此类型可以从 <seealso cref="Gua"/> 进行转换。
/// Indicates that the type could be converted from <seealso cref="Gua"/>s.
/// </summary>
/// <typeparam name="TSelf"></typeparam>
public interface IConvertableFromGua<TSelf> where TSelf : IConvertableFromGua<TSelf>
{
    /// <summary>
    /// 从 <seealso cref="Gua"/> 进行转换。
    /// Convert from a <seealso cref="Gua"/>.
    /// </summary>
    /// <param name="gua">
    /// 此卦。
    /// The Gua.
    /// </param>
    /// <param name="result">
    /// 转换结果。
    /// Conversion result.
    /// </param>
    /// <param name="message">
    /// 转换失败时提供的消息。
    /// Message provided when conversion failed.
    /// </param>
    /// <returns>
    /// 一个指示转换成功与否的值。
    /// A value indicates whether it has been successfully converted or not.
    /// </returns>
    static abstract bool TryFromGua(
        Gua gua,
        [MaybeNullWhen(false)] out TSelf result,
        [MaybeNullWhen(true)] out string message);
}