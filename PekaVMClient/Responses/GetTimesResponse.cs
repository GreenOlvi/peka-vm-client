namespace PekaVMClient.Responses;

public record struct GetTimesResponse
{
    public Bollard Bollard { get; init; }
    public IEnumerable<BusTime> Times { get; init; }
}
