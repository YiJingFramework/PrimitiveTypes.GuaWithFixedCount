using System.Diagnostics.CodeAnalysis;

namespace YiJingFramework.PrimitiveTypes.GuaWithFixedCount.Extensions;

/// <summary>
/// 此接口只是为了
/// <seealso cref="GuaAsFixedCountExtensions.As{TGuaWithFixedCount}(Gua)"/>
/// 和
/// <seealso cref="GuaAsFixedCountExtensions.TryAs{TGuaWithFixedCount}(Gua?, out TGuaWithFixedCount)"/>
/// 使用。
/// 请不要在其他场景中使用此接口。
/// This Interface is only used for
/// <seealso cref="GuaAsFixedCountExtensions.As{TGuaWithFixedCount}(Gua)"/>
/// and
/// <seealso cref="GuaAsFixedCountExtensions.TryAs{TGuaWithFixedCount}(Gua?, out TGuaWithFixedCount)"/>.
/// Do not use this interface for other purposes.
/// </summary>
/// <typeparam name="TSelf">
/// 类型自身。
/// The type itself.
/// </typeparam>
public interface IGuaWithXxxLines<TSelf> where TSelf : IGuaWithXxxLines<TSelf>
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