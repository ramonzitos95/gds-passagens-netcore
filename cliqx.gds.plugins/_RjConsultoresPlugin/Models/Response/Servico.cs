using Newtonsoft.Json;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Models.Response
{
    public class Servico
    {
        [JsonProperty("servico")]
        public string ServicoId { get; set; }

        [JsonProperty("rutaId")]
        public int RutaId { get; set; }

        [JsonProperty("prefixoLinha")]
        public string PrefixoLinha { get; set; }

        [JsonProperty("marcaId")]
        public int MarcaId { get; set; }

        [JsonProperty("grupo")]
        public string Grupo { get; set; }

        [JsonProperty("grupoOrigemId")]
        public int GrupoOrigemId { get; set; }

        [JsonProperty("grupoDestinoId")]
        public int GrupoDestinoId { get; set; }

        [JsonProperty("saida")]
        public DateTime Saida { get; set; }

        [JsonProperty("chegada")]
        public DateTime Chegada { get; set; }

        [JsonProperty("dataCorrida")]
        public DateTime DataCorrida { get; set; }

        [JsonProperty("dataSaida")]
        public DateTime DataSaida { get; set; }

        [JsonProperty("poltronasLivres")]
        public int PoltronasLivres { get; set; }

        [JsonProperty("poltronasTotal")]
        public int PoltronasTotal { get; set; }

        [JsonProperty("preco")]
        public decimal Preco { get; set; }

        [JsonProperty("precoOriginal")]
        public decimal PrecoOriginal { get; set; }

        [JsonProperty("classe")]
        public string Classe { get; set; }

        [JsonProperty("empresa")]
        public string Empresa { get; set; }

        [JsonProperty("empresaId")]
        public int EmpresaId { get; set; }

        [JsonProperty("vende")]
        public bool Vende { get; set; }

        [JsonProperty("bpe")]
        public bool Bpe { get; set; }

        [JsonProperty("km")]
        public double Km { get; set; }

        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }

        [JsonProperty("conexao")]
        public Dictionary<string, object> Conexao { get; set; }
    }
}
