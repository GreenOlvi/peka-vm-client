using System.Text.Json.Serialization;

namespace PekaVMClient.Responses;

public readonly record struct LineDirection
{
    [JsonPropertyName("returnVariant")]
    public bool IsReturnVariant { get; init; }

    public string Direction { get; init; }

    public string LineName { get; init; }
}