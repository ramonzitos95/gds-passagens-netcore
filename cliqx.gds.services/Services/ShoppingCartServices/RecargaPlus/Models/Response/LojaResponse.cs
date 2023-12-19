using cliqx.gds.contract.GdsModels;
using Newtonsoft.Json;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models.Response;
public class LojaResponse
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("nome")]
    public string Nome { get; set; }

    [JsonProperty("ativo")]
    public string Ativo { get; set; }

    [JsonProperty("data_cadastro")]
    public DateTime DataCadastro { get; set; }

    public Store AsOmsHubStore()
    {
        return new Store()
        {
            Id = Id.ToString(),
            Name = Nome
        };
    }
}
