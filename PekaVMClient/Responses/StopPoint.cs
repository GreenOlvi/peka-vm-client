namespace PekaVMClient.Responses;

public record struct StopPoint
{
    public string Symbol { get; init; }
    public string Name { get; init; }
}