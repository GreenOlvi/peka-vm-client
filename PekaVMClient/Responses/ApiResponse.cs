namespace PekaVMClient.Responses;

public record struct ApiResponse<T>
{
    public bool IsSuccess => Success != null;
    public T? Success { get; init; }
    public string? Failure { get; init; }
}
