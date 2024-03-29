﻿using System.Diagnostics.CodeAnalysis;

namespace YiJingFramework.PrimitiveTypes.GuaWithFixedCount.Extensions;

/// <summary>
/// 提供 <seealso cref="AsFixed{TGuaWithFixedCount}(Gua)"/> 的扩展。
/// Extension class that provides <seealso cref="AsFixed{TGuaWithFixedCount}(Gua)"/>.
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
    /// <exception cref="ArgumentNullException">
    /// <paramref name="gua"/> 是 <c>null</c> 。
    /// <paramref name="gua"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// 转换失败。
    /// Conversion failed.
    /// </exception>
    public static TGuaWithFixedCount AsFixed<TGuaWithFixedCount>(this Gua gua)
        where TGuaWithFixedCount : IGuaWithFixedCount<TGuaWithFixedCount>
    {
        ArgumentNullException.ThrowIfNull(gua);

        if (!TGuaWithFixedCount.TryFromGua(gua, out var result))
        {
            throw new InvalidCastException(
                $"Cannot convert Gua '{gua}' to a {typeof(TGuaWithFixedCount).Name} " +
                $"because it does not have exactly {TGuaWithFixedCount.ExpectedCount} Yaos.");
        }
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
    public static bool TryAsFixed<TGuaWithFixedCount>(
        this Gua? gua, [MaybeNullWhen(false)] out TGuaWithFixedCount result)
        where TGuaWithFixedCount : IGuaWithFixedCount<TGuaWithFixedCount>
    {
        return TGuaWithFixedCount.TryFromGua(gua, out result);
    }
}
