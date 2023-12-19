using Newtonsoft.Json;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Models.Response
{
    public class Bpe
    {
        [JsonProperty("cabecalhoAgencia")]
        public CabecalhoAgencia CabecalhoAgencia { get; set; }

        [JsonProperty("cabecalhoEmitente")]
        public CabecalhoEmitente CabecalhoEmitente { get; set; }

        [JsonProperty("qrCode")]
        public string QrCode { get; set; }

        [JsonProperty("qrcodeBpe")]
        public string QrcodeBpe { get; set; }

        [JsonProperty("chaveBpe")]
        public string ChaveBpe { get; set; }

        [JsonProperty("serieBPe")]
        public string SerieBPe { get; set; }

        [JsonProperty("numeroBPe")]
        public string NumeroBPe { get; set; }

        [JsonProperty("codigoANTTOrigem")]
        public int CodigoANTTOrigem { get; set; }

        [JsonProperty("codigoANTTDestino")]
        public int CodigoANTTDestino { get; set; }

        [JsonProperty("codigoMonitriipBPe")]
        public string CodigoMonitriipBPe { get; set; }

        [JsonProperty("contingencia")]
        public bool Contingencia { get; set; }

        [JsonProperty("formaPagamento")]
        public string FormaPagamento { get; set; }

        [JsonProperty("linha")]
        public string Linha { get; set; }

        [JsonProperty("numeroSistema")]
        public string NumeroSistema { get; set; }

        [JsonProperty("tipoServico")]
        public string TipoServico { get; set; }

        [JsonProperty("tarifa")]
        public double Tarifa { get; set; }

        [JsonProperty("seguro")]
        public double Seguro { get; set; }

        [JsonProperty("embarque")]
        public double Embarque { get; set; }

        [JsonProperty("pedagio")]
        public double Pedagio { get; set; }

        [JsonProperty("outros")]
        public double Outros { get; set; }

        [JsonProperty("desconto")]
        public double Desconto { get; set; }

        [JsonProperty("troco")]
        public int Troco { get; set; }

        [JsonProperty("valorFormaPagamento")]
        public double ValorFormaPagamento { get; set; }

        [JsonProperty("valorPagar")]
        public double ValorPagar { get; set; }

        [JsonProperty("valorTotal")]
        public double ValorTotal { get; set; }

        [JsonProperty("outrosTributos")]
        public string OutrosTributos { get; set; }

        [JsonProperty("classe")]
        public string Classe { get; set; }

        [JsonProperty("plataforma")]
        public string Plataforma { get; set; }

        [JsonProperty("prefixo")]
        public string Prefixo { get; set; }

        [JsonProperty("protocoloAutorizacao")]
        public string ProtocoloAutorizacao { get; set; }

        [JsonProperty("cnpjEmpresa")]
        public string CnpjEmpresa { get; set; }

        [JsonProperty("telefoneEmpresa")]
        public string TelefoneEmpresa { get; set; }

        [JsonProperty("telefoneEmpresaSac")]
        public string TelefoneEmpresaSac { get; set; }

        [JsonProperty("tipoDesconto")]
        public string TipoDesconto { get; set; }

        [JsonProperty("orgaoId")]
        public int OrgaoId { get; set; }

        [JsonProperty("canalVenda")]
        public string CanalVenda { get; set; }

        [JsonProperty("codigoAgencia")]
        public int CodigoAgencia { get; set; }

        [JsonProperty("descricaoAgencia")]
        public string DescricaoAgencia { get; set; }

        [JsonProperty("tipoBpe")]
        public string TipoBpe { get; set; }

        [JsonProperty("qrcode")]
        public string Qrcode { get; set; }

        [JsonProperty("numeroBpe")]
        public string NumeroBpe { get; set; }

        [JsonProperty("serie")]
        public string Serie { get; set; }

        [JsonProperty("dataAutorizacao")]
        public DateTime DataAutorizacao { get; set; }

        [JsonProperty("codigoAnttOrigem")]
        public string CodigoAnttOrigem { get; set; }

        [JsonProperty("codigoAnttDestino")]
        public string CodigoAnttDestino { get; set; }

        [JsonProperty("taxaEmbarque")]
        public string TaxaEmbarque { get; set; }
    }
}
