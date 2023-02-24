namespace PekaVMClient.Responses;

public readonly record struct GetTimesResponse
{
    public Bollard Bollard { get; init; }
    public IEnumerable<BusTime> Times { get; init; }
}
