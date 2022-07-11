using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BattleshipApi.Common.Enums;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum VesselOrientation
{
    [EnumMember(Value = "Horizontal")]
    Horizontal,
    [EnumMember(Value = "Vertical")]
    Vertical
}