using cliqx.gds.plugins._SmartbusPlugin.Api;
using Refit;

namespace cliqx.gds.plugins._SmartbusPlugin.Api;

public class CliqxSmartbusApi
{
    private readonly HttpClient _httpClient;

    public CliqxSmartbusApi(HttpClient httpClient, string url)
    {
        if (httpClient == null)
            httpClient = new HttpClient();
        _httpClient = httpClient;
        _httpClient.Timeout = TimeSpan.FromMinutes(5);
        _httpClient.BaseAddress = new Uri(url.TrimEnd('/'));
        Client = RestService.For<ICliqxSmartbusApi>(_httpClient);
    }

    public ICliqxSmartbusApi Client { get; }
}
