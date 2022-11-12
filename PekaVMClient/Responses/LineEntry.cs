using System.Text.Json.Serialization;

namespace PekaVMClient.Responses;

public record struct LineEntry
{
    [JsonPropertyName("name")]
    public string Name { get; init; }
}
