using System.Text.Json.Serialization;

namespace AtmBackend.Denominations;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PayBox
{
    Notes,
    CoinsGreaterThan20mm,
    CoinsLessThanEqual20mm
}
