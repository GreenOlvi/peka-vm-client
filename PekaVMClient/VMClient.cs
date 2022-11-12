using PekaVMClient.Responses;
using System.Text.Json;

namespace PekaVMClient;

public sealed class VMClient : IAsyncDisposable
{
    private static readonly Uri BaseUrl = new("https://www.peka.poznan.pl/vm/method.vm");

    private readonly HttpClient _httpClient = new()
    {
        BaseAddress = BaseUrl,
    };
    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    public ValueTask DisposeAsync()
    {
        _httpClient.Dispose();
        return ValueTask.CompletedTask;
    }

    private async Task<ApiResponse<T>> DoQueryAsync<T>(string method, object arg)
    {
        var data = new Dictionary<string, string>
        {
            ["method"] = method,
            ["p0"] = JsonSerializer.Serialize(arg, _serializerOptions),
        };
        var content = new FormUrlEncodedContent(data);

        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            Content = content,
        };

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ApiResponse<T>>(responseJson, _serializerOptions);
    }

    private static IEnumerable<T> Unpack<T>(ApiResponse<IEnumerable<T>> response) =>
        response.IsSuccess ? response.Success! : Enumerable.Empty<T>();

    public async Task<IEnumerable<StopPoint>> GetStopPoints(string pattern) =>
        Unpack(await DoQueryAsync<IEnumerable<StopPoint>>("getStopPoints", new { pattern }));

    public async Task<IEnumerable<BollardAndDirections>> GetBollardsByStopPoint(string name) {
        var response = await DoQueryAsync<BollardsAndDirectionsResponse>("getBollardsByStopPoint", new { name });
        return response.IsSuccess
            ? response.Success!.Bollards
            : Enumerable.Empty<BollardAndDirections>();
    }

    public async Task<IEnumerable<string>> GetLinesAsync(string pattern) =>
        Unpack(await DoQueryAsync<IEnumerable<LineEntry>>("getLines", new { pattern })).Select(l => l.Name);

    public async Task<IEnumerable<BusTime>> GetTimesAsync(string tag)
    {
        var response = await DoQueryAsync<GetTimesResponse>("getTimes", new { symbol = tag });
        return response.IsSuccess
            ? response.Success!.Times
            : Enumerable.Empty<BusTime>();
    }
}