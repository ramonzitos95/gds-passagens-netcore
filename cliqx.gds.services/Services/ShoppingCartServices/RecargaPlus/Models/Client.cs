using Newtonsoft.Json;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models
{
    public class Client
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("sobrenome")]
        public string Sobrenome { get; set; }

        [JsonProperty("nomeCompleto")]
        public string NomeCompleto { get; set; }

        [JsonProperty("cpf")]
        public string Cpf { get; set; }

        [JsonProperty("ativo")]
        public string Ativo { get; set; }

        [JsonProperty("dataCadastro")]
        public DateTime DataCadastro { get; set; }

        [JsonProperty("dataUltimoAcesso")]
        public DateTime DataUltimoAcesso { get; set; }

        [JsonProperty("dataUltimoPedido")]
        public object DataUltimoPedido { get; set; }

        [JsonProperty("dataExclusao")]
        public object DataExclusao { get; set; }

        [JsonProperty("loja")]
        public Loja Loja { get; set; }

        [JsonProperty("contato")]
        public Contato Contato { get; set; }
    }
}
