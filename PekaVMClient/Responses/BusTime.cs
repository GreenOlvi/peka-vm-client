using System.Text.Json.Serialization;

namespace PekaVMClient.Responses;

public record struct BusTime
{
    [JsonPropertyName("realTime")]
    public bool IsRealTime { get; init; }

    [JsonPropertyName("driversTicketMachine")]
    public bool HasDriversTicketMachine { get; init; }

    public int Minutes { get; init; }

    public string Direction { get; init; }

    [JsonPropertyName("lowFloorBus")]
    public bool IsLowFloorBus { get; init; }

    [JsonPropertyName("onStopPoint")]
    public bool IsOnStopPoint { get; init; }

    public DateTime Departure { get; init; }

    public string Line { get; init; }

    [JsonPropertyName("lowEntranceBus")]
    public bool IsLowEntranceBus { get; init; }

    [JsonPropertyName("ticketMachine")]
    public bool HasTicketMachine { get; init; }
}