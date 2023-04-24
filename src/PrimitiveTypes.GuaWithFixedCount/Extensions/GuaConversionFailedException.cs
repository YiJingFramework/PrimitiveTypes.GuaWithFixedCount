namespace YiJingFramework.PrimitiveTypes.GuaWithFixedCount.Extensions;

/// <summary>
/// 在尝试通过 <seealso cref="GuaAsFixedCountExtensions.As{TGuaWithFixedCount}(Gua)"/> 
/// 将 <seealso cref="Gua"/> 进行转换时发生的异常。
/// Exception that occurs when failed to convert a <seealso cref="Gua"/> with 
/// <seealso cref="GuaAsFixedCountExtensions.As{TGuaWithFixedCount}(Gua)"/>.
/// </summary>
[Serializable]
public class GuaConversionFailedException : Exception
{
    internal GuaConversionFailedException(string message) : base(message) { }

    /// <inheritdoc />
    protected GuaConversionFailedException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
