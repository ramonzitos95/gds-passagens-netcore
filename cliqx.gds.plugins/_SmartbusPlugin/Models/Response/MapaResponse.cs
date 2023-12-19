using Newtonsoft.Json;

namespace cliqx.gds.plugins._SmartbusPlugin.Models.Response
{

    public class MapaResponse
    {
        [JsonProperty("data")]
        public DataMapa Data { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }
    }
    public class Assento
    {
        [JsonProperty("assento")]
        public string Numero { get; set; }

        [JsonProperty("posicaoX")]
        public int PosicaoX { get; set; }

        [JsonProperty("posicaoY")]
        public int PosicaoY { get; set; }

        [JsonProperty("posicaoZ")]
        public int PosicaoZ { get; set; }

        [JsonProperty("ocupado")]
        public bool Ocupado { get; set; }

        [JsonProperty("idValorAssento")]
        public string IdValorAssento { get; set; }

        [JsonProperty("valorAssento")]
        public string ValorAssento { get; set; }

        [JsonProperty("cota")]
        public bool Cota { get; set; }

        [JsonProperty("tipoCota")]
        public string TipoCota { get; set; }

        [JsonProperty("descricaoCota")]
        public string DescricaoCota { get; set; }

        [JsonProperty("tipoPassageiroCota")]
        public string TipoPassageiroCota { get; set; }

        [JsonProperty("descricaoPassageiroCota")]
        public string DescricaoPassageiroCota { get; set; }
    }

    public partial class DataMapa
    {
        [JsonProperty("nroServico")]
        public string NroServico { get; set; }

        [JsonProperty("servico")]
        public string Servico { get; set; }

        [JsonProperty("horaSaida")]
        public string HoraSaida { get; set; }

        [JsonProperty("horaChegada")]
        public string HoraChegada { get; set; }

        [JsonProperty("diaChegada")]
        public int DiaChegada { get; set; }

        [JsonProperty("empresaId")]
        public int EmpresaId { get; set; }

        [JsonProperty("nomeEmpresa")]
        public string NomeEmpresa { get; set; }

        [JsonProperty("preco")]
        public double Preco { get; set; }

        [JsonProperty("classeServico")]
        public string ClasseServico { get; set; }

        [JsonProperty("piso")]
        public int Piso { get; set; }

        [JsonProperty("assentos")]
        public List<Assento> assentos { get; set; }

        [JsonProperty("outros")]
        public List<Object> outros { get; set; }

        [JsonProperty("moeda")]
        public string Moeda { get; set; }

        [JsonProperty("detalhePreco")]
        public DetalhePrecoMapa DetalhePreco { get; set; }
    }

    public class DetalhePrecoMapa
    {
        [JsonProperty("tarifa")]
        public double Tarifa { get; set; }

        [JsonProperty("taxaEmbarque")]
        public double TaxaEmbarque { get; set; }

        [JsonProperty("seguroObrigatorio")]
        public double SeguroObrigatorio { get; set; }

        [JsonProperty("pedagio")]
        public double Pedagio { get; set; }

        [JsonProperty("outros")]
        public double Outros { get; set; }

        [JsonProperty("seguroOpcional")]
        public double SeguroOpcional { get; set; }

        [JsonProperty("taxaConveniencia")]
        public double TaxaConveniencia { get; set; }

        [JsonProperty("total")]
        public double Total { get; set; }
    }





}
