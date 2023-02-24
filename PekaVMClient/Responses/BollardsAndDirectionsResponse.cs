namespace PekaVMClient.Responses;

public readonly record struct BollardsAndDirectionsResponse
{
    public IEnumerable<BollardAndDirections> Bollards { get; init; }
}
