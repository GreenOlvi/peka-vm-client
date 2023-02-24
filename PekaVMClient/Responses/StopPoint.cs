namespace PekaVMClient.Responses;

public readonly record struct StopPoint
{
    public string Symbol { get; init; }
    public string Name { get; init; }
}