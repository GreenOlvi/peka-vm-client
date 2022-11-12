using System.Text.Json.Serialization;

namespace PekaVMClient.Responses;

public record struct Bollard
{
    public string Symbol { get; init; }

    public string Tag { get; init; }

    public string Name { get; init; }

    [JsonPropertyName("mainBollard")]
    public bool IsMain { get; init; }
}