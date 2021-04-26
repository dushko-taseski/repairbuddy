using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

[JsonConverter(typeof(StringEnumConverter))]
public enum TicketStatus
{
    [EnumMember(Value = "Open")]
    Open,
    [EnumMember(Value = "Closed")]
    Closed
}
