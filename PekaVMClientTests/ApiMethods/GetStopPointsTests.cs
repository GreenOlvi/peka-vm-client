using PekaVMClient.Responses;

namespace PekaVMClientTests.ApiMethods;

[TestFixture]
internal sealed class GetStopPointsTests
{
    private const string ResponseGetStopsMos =
        """{"success":[{"symbol":"MODW","name":"Most Dworcowy"},{"symbol":"MT","name":"Most Teatralny"},{"symbol":"MORO","name":"Most Św. Rocha"},{"symbol":"MOST","name":"Mostowa"}]}""";

    [Test]
    public async Task GetStopPointsForCorrectPatternTest()
    {
        var httpClient = MockHttpClient((method, arg) =>
        {
            var resp = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(ResponseGetStopsMos, Encoding.UTF8, "application/json"),
            };

            return Task.FromResult(resp);
        });

        var client = new VMClient(httpClient);
        var response = await client.GetStopPoints("Mos");
        response.Should().BeEquivalentTo(new[] {
            new StopPoint { Symbol = "MODW", Name = "Most Dworcowy" },
            new StopPoint { Symbol = "MT",   Name = "Most Teatralny" },
            new StopPoint { Symbol = "MORO", Name = "Most Św. Rocha" },
            new StopPoint { Symbol = "MOST", Name = "Mostowa" },
        });
    }
}
