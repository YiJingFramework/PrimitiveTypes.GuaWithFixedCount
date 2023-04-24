﻿using System.Diagnostics.CodeAnalysis;

namespace YiJingFramework.PrimitiveTypes.GuaWithFixedCount.Extensions;

/// <summary>
/// 提供 <seealso cref="As{TGuaWithFixedCount}(Gua)"/> 的扩展。
/// Extension that provides <seealso cref="As{TGuaWithFixedCount}(Gua)"/>.
/// </summary>
public static partial class GuaAsFixedCountExtensions
{
    /// <summary>
    /// 将 <seealso cref="Gua"/> 转换为 <typeparamref name="TGuaWithFixedCount"/> 。
    /// Convert a <seealso cref="Gua"/> to a <typeparamref name="TGuaWithFixedCount"/>.
    /// </summary>
    /// <typeparam name="TGuaWithFixedCount">
    /// 要转换到的类型。
    /// The type to convert to.
    /// </typeparam>
    /// <param name="gua">
    /// 卦。
    /// The Gua.
    /// </param>
    /// <returns>
    /// 结果。
    /// The result.
    /// </returns>
    /// <exception cref="GuaConversionFailedException">
    /// 转换失败。
    /// Conversion failed.
    /// </exception>
    public static TGuaWithFixedCount As<TGuaWithFixedCount>(this Gua gua)
        where TGuaWithFixedCount : IConvertableFromGua<TGuaWithFixedCount>
    {
        ArgumentNullException.ThrowIfNull(gua);

        if (!TGuaWithFixedCount.TryFromGua(gua, out var result, out var message))
            throw new GuaConversionFailedException(message);
        return result;
    }

    /// <summary>
    /// 将 <seealso cref="Gua"/> 转换为 <typeparamref name="TGuaWithFixedCount"/> 。
    /// Convert a <seealso cref="Gua"/> to a <typeparamref name="TGuaWithFixedCount"/>.
    /// </summary>
    /// <typeparam name="TGuaWithFixedCount">
    /// 要转换到的类型。
    /// The type to convert to.
    /// </typeparam>
    /// <param name="gua">
    /// 卦。
    /// The Gua.
    /// </param>
    /// <param name="result">
    /// 结果。
    /// The result.
    /// </param>
    /// <returns>
    /// 一个指示转换成功与否的值。
    /// A value indicates whether it has been successfully converted or not.
    /// </returns>
    public static bool TryAs<TGuaWithFixedCount>(
        this Gua gua, [MaybeNullWhen(false)] out TGuaWithFixedCount result)
        where TGuaWithFixedCount : IConvertableFromGua<TGuaWithFixedCount>
    {
        return TGuaWithFixedCount.TryFromGua(gua, out result, out _);
    }
}
