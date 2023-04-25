#nullable enable

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Text.Json.Serialization;
using YiJingFramework.PrimitiveTypes.GuaWithFixedCount.Extensions;
using YiJingFramework.PrimitiveTypes.Serialization;

namespace YiJingFramework.PrimitiveTypes.GuaWithFixedCount;

/// <summary>
/// 有 <c>999</c> 根爻的卦。
/// 爻位置越低，序号越小。
/// A Gua with exactly <c>999</c> lines.
/// The lower a line, the smaller its index.
/// </summary>
[JsonConverter(typeof(JsonConverterOfStringConvertibleForJson<GuaWith999Lines>))]
public sealed partial class GuaWith999Lines :
    IReadOnlyList<Yinyang>, IComparable<GuaWith999Lines>, IEquatable<GuaWith999Lines>,
    IParsable<GuaWith999Lines>, IEqualityOperators<GuaWith999Lines, GuaWith999Lines, bool>,
    IStringConvertibleForJson<GuaWith999Lines>, IConvertableFromGua<GuaWith999Lines>
{
    private readonly Gua innerGua;

    /// <summary>
    /// 创建新实例。
    /// Initializes a new instance.
    /// </summary>
    /// <param name="lines">
    /// 各爻的性质。
    /// The lines' attributes.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="lines"/> 是 <c>null</c> 。
    /// <paramref name="lines"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="lines"/> 的长度不是 <c>999</c> 。
    /// <paramref name="lines"/> doesn't exactly contains <c>999</c> Yinyangs.
    /// </exception>
    public GuaWith999Lines(IEnumerable<Yinyang> lines)
    {
        ArgumentNullException.ThrowIfNull(lines);
        this.innerGua = new Gua(lines);
        if (this.innerGua.Count is not 999)
        {
            throw new ArgumentException(
                $"{nameof(lines)} should exactly contains 999 Yinyangs.", nameof(lines));
        }
    }

    #region Collecting
    /// <summary>
    /// 获取某一根爻的性质。
    /// Get the attribute of a line.
    /// </summary>
    /// <param name="index">
    /// 爻的序号。
    /// The index of the line.
    /// </param>
    /// <returns>
    /// 爻的性质。
    /// The line.
    /// </returns>
    /// <exception cref="IndexOutOfRangeException">
    /// <paramref name="index"/> 超出范围。
    /// <paramref name="index"/> is out of range.
    /// </exception>
    public Yinyang this[int index] => this.innerGua[index];

    /// <summary>
    /// 获取爻的个数。
    /// Get the count of the lines.
    /// </summary>
    public int Count => 999;

    /// <inheritdoc/>
    public IEnumerator<Yinyang> GetEnumerator()
    {
        return this.innerGua.GetEnumerator();
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.innerGua.GetEnumerator();
    }
    #endregion

    #region Comparing

    /// <inheritdoc/>
    public int CompareTo(GuaWith999Lines? other)
    {
        return this.innerGua.CompareTo(other?.innerGua);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return this.innerGua.GetHashCode();
    }

    /// <inheritdoc/>
    public override bool Equals(object? other)
    {
        if (other is GuaWith999Lines gua)
            return this.innerGua.Equals(gua.innerGua);
        return false;
    }

    /// <inheritdoc/>
    public bool Equals(GuaWith999Lines? other)
    {
        return this.innerGua.Equals(other?.innerGua);
    }

    /// <inheritdoc/>
    public static bool operator ==(GuaWith999Lines? left, GuaWith999Lines? right)
    {
        return left?.innerGua == right?.innerGua;
    }

    /// <inheritdoc/>
    public static bool operator !=(GuaWith999Lines? left, GuaWith999Lines? right)
    {
        return left?.innerGua != right?.innerGua;
    }
    #endregion

    #region Converting
    /// <inheritdoc/>
    public override string ToString()
    {
        return this.innerGua.ToString();
    }


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
    public static GuaWith999Lines Parse(string s)
    {
        if (!Gua.TryParse(s, out var gua) || !TryFromGua(gua, out var result))
            throw new FormatException($"Cannot parse \"{s}\" as {nameof(GuaWith999Lines)}.");
        return result;
    }

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
    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        [MaybeNullWhen(false)] out GuaWith999Lines result)
    {
        result = null;
        return Gua.TryParse(s, out var gua) && TryFromGua(gua, out result);
    }

    static GuaWith999Lines IParsable<GuaWith999Lines>.Parse(
        string s, IFormatProvider? provider)
    {
        return Parse(s);
    }

    static bool IParsable<GuaWith999Lines>.TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out GuaWith999Lines result)
    {
        return TryParse(s, out result);
    }
    #endregion

    #region Serializing
    static bool IStringConvertibleForJson<GuaWith999Lines>.FromStringForJson(
        string s, [MaybeNullWhen(false)] out GuaWith999Lines result)
    {
        return TryParse(s, out result);
    }

    string IStringConvertibleForJson<GuaWith999Lines>.ToStringForJson()
    {
        return this.ToString();
    }
    #endregion

    #region Converting with Guas
    /// <summary>
    /// 转换为 <seealso cref="Gua"/> 。
    /// Convert to <seealso cref="Gua"/>.
    /// </summary>
    /// <returns>
    /// 卦。
    /// A Gua.
    /// </returns>
    public Gua AsGua()
    {
        return this.innerGua;
    }

    private static bool TryFromGua(Gua gua, [MaybeNullWhen(false)] out GuaWith999Lines result)
    {
        if (gua.Count is not 999)
        {
            result = null;
            return false;
        }
        result = new(gua);
        return true;
    }

    static bool IConvertableFromGua<GuaWith999Lines>.TryFromGua(
        Gua gua,
        [MaybeNullWhen(false)] out GuaWith999Lines result,
        [MaybeNullWhen(true)] out string message)
    {
        if (!TryFromGua(gua, out result))
        {
            message = $"Cannot convert Gua '{gua}' to GuaWith999Lines " +
                $"because its count of lines is not 999.";
            return false;
        }
        message = null;
        return true;
    }
    #endregion
}
