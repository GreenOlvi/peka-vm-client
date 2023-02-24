using System.Text.Json.Serialization;

namespace PekaVMClient.Responses;

public readonly record struct LineEntry
{
    [JsonPropertyName("name")]
    public string Name { get; init; }
}
