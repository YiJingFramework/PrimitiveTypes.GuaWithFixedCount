#nullable enable

using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace YiJingFramework.PrimitiveTypes.GuaWithFixedCount;

/// <summary>
/// 有 <c>999</c> 根爻的卦。
/// 爻位置越低，序号越小。
/// A Gua with exactly <c>999</c> Yaos.
/// The lower the position of a Yao, the smaller its index.
/// </summary>
public sealed partial class GuaWith999Yaos : IGuaWithFixedCount<GuaWith999Yaos>
{
    private readonly Gua innerGua;

    /// <exception cref="ArgumentException">
    /// <paramref name="yaos"/> 的长度不是 <c>999</c> 。
    /// <paramref name="yaos"/> doesn't exactly contains <c>999</c> Yinyangs.
    /// </exception>
    /// <inheritdoc cref="Gua(IEnumerable{Yinyang})" />
    public GuaWith999Yaos(IEnumerable<Yinyang> yaos)
    {
        ArgumentNullException.ThrowIfNull(yaos);

        if (yaos is Gua gua)
            this.innerGua = gua;
        else if (yaos is ImmutableArray<Yinyang> array)
            this.innerGua = new Gua(array);
        else
            this.innerGua = new Gua(yaos);

        if (this.innerGua.Count is not 999)
        {
            throw new ArgumentException(
                $"{nameof(yaos)} should exactly contains 999 Yaos.", nameof(yaos));
        }
    }

    #region Collecting
    /// <inheritdoc cref="Gua.this[int]" />
    public Yinyang this[int index] => this.innerGua[index];

    /// <inheritdoc cref="Gua.Count" />
    public int Count => 999;

    static int IGuaWithFixedCount<GuaWith999Yaos>.ExpectedCount => 999;

    /// <inheritdoc />
    public IEnumerator<Yinyang> GetEnumerator()
    {
        return this.innerGua.GetEnumerator();
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.innerGua.GetEnumerator();
    }
    #endregion

    #region Comparing

    /// <inheritdoc />
    public int CompareTo(GuaWith999Yaos? other)
    {
        return this.innerGua.CompareTo(other?.innerGua);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return this.innerGua.GetHashCode();
    }

    /// <inheritdoc />
    public override bool Equals(object? other)
    {
        if (other is GuaWith999Yaos gua)
            return this.innerGua.Equals(gua.innerGua);
        return false;
    }

    /// <inheritdoc />
    public bool Equals(GuaWith999Yaos? other)
    {
        return this.innerGua.Equals(other?.innerGua);
    }

    /// <inheritdoc />
    public static bool operator ==(GuaWith999Yaos? left, GuaWith999Yaos? right)
    {
        return left?.innerGua == right?.innerGua;
    }

    /// <inheritdoc />
    public static bool operator !=(GuaWith999Yaos? left, GuaWith999Yaos? right)
    {
        return left?.innerGua != right?.innerGua;
    }
    #endregion

    #region Converting
    /// <inheritdoc />
    public override string ToString()
    {
        return this.innerGua.ToString();
    }

    /// <inheritdoc />
    public static GuaWith999Yaos Parse(string s)
    {
        ArgumentNullException.ThrowIfNull(s);

        s = s.Trim();
        if (s.Length is not 999)
            throw new FormatException($"Cannot parse \"{s}\" as {nameof(GuaWith999Yaos)}.");

        var r = ImmutableArray.CreateBuilder<Yinyang>(999);
        foreach (var c in s)
        {
            r.Add(c switch
            {
                '0' => Yinyang.Yin,
                '1' => Yinyang.Yang,
                _ => throw new FormatException($"Cannot parse \"{s}\" as {nameof(GuaWith999Yaos)}.")
            });
        }
        return new(r.MoveToImmutable());
    }

    /// <inheritdoc />
    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        [MaybeNullWhen(false)] out GuaWith999Yaos result)
    {
        if (s?.Trim().Length is not 999)
        {
            result = null;
            return false;
        }

        var r = ImmutableArray.CreateBuilder<Yinyang>(999);
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
        result = new(r.MoveToImmutable());
        return true;
    }

    static GuaWith999Yaos IParsable<GuaWith999Yaos>.Parse(
        string s, IFormatProvider? provider)
    {
        return Parse(s);
    }

    static bool IParsable<GuaWith999Yaos>.TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out GuaWith999Yaos result)
    {
        return TryParse(s, out result);
    }
    #endregion

    #region Converting with Guas
    /// <inheritdoc />
    public Gua AsGua()
    {
        return this.innerGua;
    }

    private GuaWith999Yaos(Gua guaLengthChecked)
    {
        this.innerGua = guaLengthChecked;
    }

    /// <inheritdoc />
    public static bool TryFromGua(Gua? gua, [MaybeNullWhen(false)] out GuaWith999Yaos result)
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
    public static GuaWith999Yaos operator &(GuaWith999Yaos left, GuaWith999Yaos right)
    {
        static IEnumerable<Yinyang> Calculate(GuaWith999Yaos g1, GuaWith999Yaos g2)
        {
            foreach (var (y1, y2) in g1.Zip(g2))
                yield return y1 & y2;
        }
        return new GuaWith999Yaos(Calculate(left, right));
    }

    /// <inheritdoc/>
    public static GuaWith999Yaos operator |(GuaWith999Yaos left, GuaWith999Yaos right)
    {
        static IEnumerable<Yinyang> Calculate(GuaWith999Yaos g1, GuaWith999Yaos g2)
        {
            foreach (var (y1, y2) in g1.Zip(g2))
                yield return y1 | y2;
        }
        return new GuaWith999Yaos(Calculate(left, right));
    }

    /// <inheritdoc/>
    public static GuaWith999Yaos operator ^(GuaWith999Yaos left, GuaWith999Yaos right)
    {
        static IEnumerable<Yinyang> Calculate(GuaWith999Yaos g1, GuaWith999Yaos g2)
        {
            foreach (var (y1, y2) in g1.Zip(g2))
                yield return y1 ^ y2;
        }
        return new GuaWith999Yaos(Calculate(left, right));
    }

    /// <inheritdoc/>
    public static GuaWith999Yaos operator ~(GuaWith999Yaos gua)
    {
        static IEnumerable<Yinyang> Calculate(GuaWith999Yaos g)
        {
            foreach (var y in g)
                yield return !y;
        }
        return new GuaWith999Yaos(Calculate(gua));
    }
    #endregion
}
