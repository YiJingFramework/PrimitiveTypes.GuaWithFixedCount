using System.Diagnostics;
using System.Text.Json;
using YiJingFramework.PrimitiveTypes;
using YiJingFramework.PrimitiveTypes.GuaWithFixedCount;
using YiJingFramework.PrimitiveTypes.GuaWithFixedCount.Extensions;

#region all types
_ = new[] {
    typeof(GuaEmpty),
    typeof(GuaWith1Line),
    typeof(GuaWith2Lines),
    typeof(GuaTrigram),
    typeof(GuaWith4Lines),
    typeof(GuaWith5Lines),
    typeof(GuaHexagram),
    typeof(GuaWith7Lines),
    typeof(GuaWith8Lines),
    typeof(GuaWith9Lines)
};
#endregion


#region just use them like using Guas
// create
_ = new GuaWith9Lines(
    Yinyang.Yin, Yinyang.Yin, Yinyang.Yang,
    Yinyang.Yin, Yinyang.Yang, Yinyang.Yin,
    Yinyang.Yang, Yinyang.Yin, Yinyang.Yin);

var trigram = new GuaTrigram(Yinyang.Yin, Yinyang.Yang, Yinyang.Yang);

// json (de)serializable
var serialized = JsonSerializer.Serialize(trigram);
var deserialized = JsonSerializer.Deserialize<GuaTrigram>(serialized);
Debug.Assert(trigram == deserialized);

// use as lists of lines
Debug.Assert(trigram.Count == 3);
Debug.Assert(trigram[0] == Yinyang.Yin);

// convert to strings and parse back
Console.WriteLine(GuaTrigram.Parse("101"));
// Output: 101

// Differences between GuaWithXxxLines and Gua:
// 1. GuaWithXxxLines does not provides ToBytes and FromBytes methods
// 2. If obj is an instance of Gua, GuaWithXxxLines.Equals((object)obj) will always return false 
//    * (It could return true when obj is GuaWithXxxLines)
//    * (Gua and GuaWithXxxLines should be regarded as completely unrelated types.)
#endregion

#region could be converted to Guas and back
var gua = trigram.AsGua();
var c = gua.AsFixed<GuaTrigram>();
Debug.Assert(trigram == c);
#endregion