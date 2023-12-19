using Newtonsoft.Json;

namespace cliqx.gds.plugins._SmartbusPlugin.Models.Response
{
    public partial class ReservaResponse
    {
        [JsonProperty("data")]
        public DataReserva Data { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }
    }

    public class DataReserva
    {
        [JsonProperty("transacaoId")]
        public string TransacaoId { get; set; }

        [JsonProperty("tarifa")]
        public double Tarifa { get; set; }

        [JsonProperty("taxaEmbarque")]
        public double TaxaEmbarque { get; set; }

        [JsonProperty("pedagio")]
        public double Pedagio { get; set; }

        [JsonProperty("seguroObrigatorio")]
        public double SeguroObrigatorio { get; set; }

        [JsonProperty("seguroOpcional")]
        public object SeguroOpcional { get; set; }

        [JsonProperty("outros")]
        public double Outros { get; set; }

        [JsonProperty("valorAssento")]
        public object ValorAssento { get; set; }

        [JsonProperty("taxaServico")]
        public double TaxaServico { get; set; }

        [JsonProperty("moeda")]
        public string Moeda { get; set; }
    }


}
