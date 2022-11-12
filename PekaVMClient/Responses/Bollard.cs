using System.Text.Json.Serialization;

namespace PekaVMClient.Responses;

public class Bollard
{
    public string Symbol { get; init; } = string.Empty;

    public string Tag { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("mainBollard")]
    public bool IsMain { get; init; }
}