using System;
using Newtonsoft.Json;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models.Response;
public class ConfiguracaoResponse
{
    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public int id { get; set; }

    [JsonProperty("relLojaId", NullValueHandling = NullValueHandling.Ignore)]
    public int relLojaId { get; set; }

    [JsonProperty("loja", NullValueHandling = NullValueHandling.Ignore)]
    public object loja { get; set; }

    [JsonProperty("chave", NullValueHandling = NullValueHandling.Ignore)]
    public string chave { get; set; }

    [JsonProperty("valor", NullValueHandling = NullValueHandling.Ignore)]
    public string valor { get; set; }
}
