using cliqx.gds.plugins._SmartbusPlugin.Models.Request;
using cliqx.gds.plugins._SmartbusPlugin.Models.Response;
using Refit;

namespace cliqx.gds.plugins._SmartbusPlugin.Api;

public interface ICliqxSmartbusApi
{
    [Get("/api/getAllByCityName")]
    public Task<ApiResponse<List<LocalidadeResponse>>> GetAllByCityName(string cityName);
}
