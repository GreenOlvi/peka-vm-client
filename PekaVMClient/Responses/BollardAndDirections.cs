namespace PekaVMClient.Responses;

public record struct BollardAndDirections
{
    public Bollard Bollard { get; init; }
    public IEnumerable<LineDirection> Directions { get; init; }
}
