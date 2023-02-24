using PekaVMClient.Responses;
using System.Text.Json;

namespace PekaVMClient;

public sealed class VMClient : IAsyncDisposable, IDisposable
{
    private static readonly Uri BaseUrl = new("https://www.peka.poznan.pl/vm/method.vm");

    private readonly bool _disposeClient = true;
    private readonly HttpClient _httpClient = new()
    {
        BaseAddress = BaseUrl,
    };

    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    /// <summary>
    /// Creates new instance of VMClient with default HttpClient
    /// </summary>
    public VMClient()
    {
    }

    /// <summary>
    /// Creates new instance of VMClient with given HttpClient
    /// </summary>
    /// <param name="httpClient"></param>
    public VMClient(HttpClient httpClient, bool disposeClient = false)
    {
        _httpClient = httpClient;
        _disposeClient = disposeClient;
    }

    /// <summary>
    /// Creates new instance of VMClient with given HttpClient factory
    /// </summary>
    /// <param name="httpClientFactory">HttpClient factory</param>
    public VMClient(Func<HttpClient> httpClientFactory)
    {
        _httpClient = httpClientFactory();
    }

    public ValueTask DisposeAsync()
    {
        Dispose();
        return ValueTask.CompletedTask;
    }

    public void Dispose()
    {
        if (_disposeClient)
        {
            _httpClient.Dispose();
        }
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

    private static IEnumerable<T> UnpackResponse<T>(ApiResponse<IEnumerable<T>> response) =>
        response.IsSuccess ? response.Success! : Enumerable.Empty<T>();

    /// <summary>
    /// Returns collection of stop points matching given pattern
    /// </summary>
    /// <param name="pattern">Pattern to match stop points</param>
    /// <returns>Collection of stop points</returns>
    public async Task<IEnumerable<StopPoint>> GetStopPoints(string pattern) =>
        UnpackResponse(await DoQueryAsync<IEnumerable<StopPoint>>("getStopPoints", new { pattern }));

    /// <summary>
    /// Returns collection of bollards from a stop point
    /// </summary>
    /// <param name="name">Stop point name</param>
    /// <returns>Collection of bollards with directions</returns>
    public async Task<IEnumerable<BollardAndDirections>> GetBollardsByStopPoint(string name) {
        var response = await DoQueryAsync<BollardsAndDirectionsResponse>("getBollardsByStopPoint", new { name });
        return response.IsSuccess && response.Success.Bollards is not null
            ? response.Success.Bollards
            : Enumerable.Empty<BollardAndDirections>();
    }

    /// <summary>
    /// Returns collection of bollards by street name
    /// </summary>
    /// <param name="name">Street name</param>
    /// <returns>Collection of bollards with directions</returns>
    public async Task<IEnumerable<BollardAndDirections>> GetBollardsByStreet(string name)
    {
        var response = await DoQueryAsync<BollardsAndDirectionsResponse>("getBollardsByStreet", new { name });
        return response.IsSuccess && response.Success.Bollards is not null
            ? response.Success.Bollards
            : Enumerable.Empty<BollardAndDirections>();
    }

    /// <summary>
    /// Returns collection of lines matching given pattern
    /// </summary>
    /// <param name="pattern">Pattern to match lines</param>
    /// <returns>Collection of lines</returns>
    public async Task<IEnumerable<string>> GetLinesAsync(string pattern) =>
        UnpackResponse(await DoQueryAsync<IEnumerable<LineEntry>>("getLines", new { pattern })).Select(l => l.Name);

    /// <summary>
    /// Returns collection of bus times by tag
    /// </summary>
    /// <param name="tag">Tag</param>
    /// <returns></returns>
    public async Task<IEnumerable<BusTime>> GetTimesByBollardAsync(Bollard bollard)
    {
        var response = await DoQueryAsync<GetTimesResponse>("getTimes", new { symbol = bollard.Tag });
        return response.IsSuccess
            ? response.Success!.Times
            : Enumerable.Empty<BusTime>();
    }
}