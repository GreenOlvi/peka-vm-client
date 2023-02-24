namespace PekaVMClient.Responses;

public readonly record struct BollardAndDirections
{
    public Bollard Bollard { get; init; }
    public IEnumerable<LineDirection> Directions { get; init; }
}
