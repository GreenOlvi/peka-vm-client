using System.Text.Json.Serialization;

namespace PekaVMClient.Responses;

public class BusTime
{
    [JsonPropertyName("realTime")]
    public bool IsRealTime { get; init; }

    [JsonPropertyName("driversTicketMachine")]
    public bool HasDriversTicketMachine { get; init; }

    public int Minutes { get; init; }

    public string Direction { get; init; } = string.Empty;

    [JsonPropertyName("lowFloorBus")]
    public bool IsLowFloorBus { get; init; }

    [JsonPropertyName("onStopPoint")]
    public bool IsOnStopPoint { get; init; }

    public DateTime Departure { get; init; }

    public string Line { get; init; } = string.Empty;

    [JsonPropertyName("lowEntranceBus")]
    public bool IsLowEntranceBus { get; init; }

    [JsonPropertyName("ticketMachine")]
    public bool HasTicketMachine { get; init; }
}