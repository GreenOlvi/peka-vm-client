using System.Text.Json;
using System.Web;

namespace PekaVMClientTests;

public static class VMClientTestsHelper
{
    public const string BaseAddress = "http://localhost/vm/method.vm";

    public static HttpClient MockHttpClient(Func<string, Dictionary<string, string>, Task<HttpResponseMessage>> handler)
    {
        var msgHandler = new MockHttpMessageHandler();
        msgHandler.Expect(HttpMethod.Post, BaseAddress)
            .Respond(async req => {
                var content = await req.Content!.ReadAsStringAsync();
                var (method, args) = DecodeRequest(content);
                return await handler(method, args);
            });

        return new HttpClient(msgHandler)
        {
            BaseAddress = new Uri(BaseAddress),
        };
    }

    public static (string Method, Dictionary<string, string> Args) DecodeRequest(string requestContent)
    {
        var decoded = HttpUtility.ParseQueryString(requestContent);
        var args = JsonSerializer.Deserialize<Dictionary<string, string>>(decoded["p0"]!);
        return (decoded["method"]!, args!);
    }
}