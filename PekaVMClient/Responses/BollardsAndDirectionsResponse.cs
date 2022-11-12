namespace PekaVMClient.Responses;

public record struct BollardsAndDirectionsResponse
{
    public IEnumerable<BollardAndDirections> Bollards { get; init; }
}
