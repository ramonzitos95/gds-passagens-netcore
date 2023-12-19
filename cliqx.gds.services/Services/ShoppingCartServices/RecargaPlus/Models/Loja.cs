using System;
using Newtonsoft.Json;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models;
public class Loja
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("nome")]
    public string Nome { get; set; }

    [JsonProperty("ativo")]
    public string Ativo { get; set; }

    [JsonProperty("data_cadastro")]
    public DateTime DataCadastro { get; set; }
}

