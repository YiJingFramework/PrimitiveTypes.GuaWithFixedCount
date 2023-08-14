﻿#nullable enable

using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using YiJingFramework.PrimitiveTypes.Serialization;

namespace YiJingFramework.PrimitiveTypes.GuaWithFixedCount;

/// <summary>
/// 有 <c>999</c> 根爻的卦。
/// 爻位置越低，序号越小。
/// A Gua with exactly <c>999</c> lines.
/// The lower a line, the smaller its index.
/// </summary>
[JsonConverter(typeof(JsonConverterOfStringConvertibleForJson<GuaWith999Lines>))]
public sealed partial class GuaWith999Lines : IGuaWithFixedCount<GuaWith999Lines>
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
        this.innerGua = lines is Gua gua ? gua : new Gua(lines);
        if (this.innerGua.Count is not 999)
        {
            throw new ArgumentException(
                $"{nameof(lines)} should exactly contains 999 Yinyangs.", nameof(lines));
        }
    }

    #region Collecting
    /// <inheritdoc cref="Gua.this[int]" />
    public Yinyang this[int index] => this.innerGua[index];

    /// <inheritdoc cref="Gua.Count" />
    public int Count => 999;

    static int IGuaWithFixedCount<GuaWith999Lines>.ExpectedCount => 999;

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

    /// <inheritdoc />
    public static GuaWith999Lines Parse(string s)
    {
        ArgumentNullException.ThrowIfNull(s);

        s = s.Trim();
        if (s.Length is not 999)
            throw new FormatException($"Cannot parse \"{s}\" as {nameof(GuaWith999Lines)}.");

        List<Yinyang> r = new(999);
        foreach (var c in s)
        {
            r.Add(c switch
            {
                '0' => Yinyang.Yin,
                '1' => Yinyang.Yang,
                _ => throw new FormatException($"Cannot parse \"{s}\" as {nameof(GuaWith999Lines)}.")
            });
        }
        return new(r);
    }

    /// <inheritdoc />
    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        [MaybeNullWhen(false)] out GuaWith999Lines result)
    {
        if (s?.Trim().Length is not 999)
        {
            result = null;
            return false;
        }

        List<Yinyang> r = new(999);
        foreach (var c in s)
        {
            switch (c)
            {
                case '0':
                    r.Add(Yinyang.Yin);
                    break;
                case '1':
                    r.Add(Yinyang.Yang);
                    break;
                default:
                    result = null;
                    return false;
            }
        }
        result = new(r);
        return true;
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
    /// <inheritdoc />
    public Gua AsGua()
    {
        return this.innerGua;
    }

    private GuaWith999Lines(Gua guaLengthChecked)
    {
        this.innerGua = guaLengthChecked;
    }

    /// <inheritdoc />
    public static bool TryFromGua(Gua? gua, [MaybeNullWhen(false)] out GuaWith999Lines result)
    {
        if (gua?.Count is not 999)
        {
            result = null;
            return false;
        }
        result = new(gua);
        return true;
    }
    #endregion

    #region calculating
    /// <inheritdoc/>
    public static GuaWith999Lines operator &(GuaWith999Lines left, GuaWith999Lines right)
    {
        static IEnumerable<Yinyang> Calculate(GuaWith999Lines g1, GuaWith999Lines g2)
        {
            foreach (var (y1, y2) in g1.Zip(g2))
                yield return y1 & y2;
        }
        return new GuaWith999Lines(Calculate(left, right));
    }

    /// <inheritdoc/>
    public static GuaWith999Lines operator |(GuaWith999Lines left, GuaWith999Lines right)
    {
        static IEnumerable<Yinyang> Calculate(GuaWith999Lines g1, GuaWith999Lines g2)
        {
            foreach (var (y1, y2) in g1.Zip(g2))
                yield return y1 | y2;
        }
        return new GuaWith999Lines(Calculate(left, right));
    }

    /// <inheritdoc/>
    public static GuaWith999Lines operator ^(GuaWith999Lines left, GuaWith999Lines right)
    {
        static IEnumerable<Yinyang> Calculate(GuaWith999Lines g1, GuaWith999Lines g2)
        {
            foreach (var (y1, y2) in g1.Zip(g2))
                yield return y1 ^ y2;
        }
        return new GuaWith999Lines(Calculate(left, right));
    }

    /// <inheritdoc/>
    public static GuaWith999Lines operator ~(GuaWith999Lines gua)
    {
        static IEnumerable<Yinyang> Calculate(GuaWith999Lines g)
        {
            foreach (var y in g)
                yield return !y;
        }
        return new GuaWith999Lines(Calculate(gua));
    }
    #endregion
}
