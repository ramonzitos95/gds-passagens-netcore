

using Newtonsoft.Json;

namespace cliqx.gds.plugins._SmartbusPlugin.Models.Response
{
    public partial class ConfirmacaoResponse
    {
        [JsonProperty("data")]
        public DataConfirmacao Data { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }
    }

    public class DataConfirmacao
    {
        [JsonProperty("localizador")]
        public string Localizador { get; set; }

        [JsonProperty("bilhete")]
        public string Bilhete { get; set; }

        [JsonProperty("assento")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Assento { get; set; }

        [JsonProperty("tipoServico")]
        public string TipoServico { get; set; }

        [JsonProperty("classeServico")]
        public string ClasseServico { get; set; }

        [JsonProperty("razaoSocialAgencia")]
        public string RazaoSocialAgencia { get; set; }

        [JsonProperty("cnpjAgencia")]
        public string CnpjAgencia { get; set; }

        [JsonProperty("inscrEstadualAgencia")]
        public string InscrEstadualAgencia { get; set; }

        [JsonProperty("telefoneAgencia")]
        public string TelefoneAgencia { get; set; }

        [JsonProperty("enderecoAgencia")]
        public string EnderecoAgencia { get; set; }

        [JsonProperty("bairroAgencia")]
        public string BairroAgencia { get; set; }

        [JsonProperty("numeroAgencia")]
        public string NumeroAgencia { get; set; }

        [JsonProperty("cidadeAgencia")]
        public string CidadeAgencia { get; set; }

        [JsonProperty("ufAgencia")]
        public string UfAgencia { get; set; }

        [JsonProperty("cepAgencia")]
        public string CepAgencia { get; set; }

        [JsonProperty("razaoSocialEmpresa")]
        public string RazaoSocialEmpresa { get; set; }

        [JsonProperty("cnpjEmpresa")]
        public string CnpjEmpresa { get; set; }

        [JsonProperty("inscrEstadualEmpresa")]
        public string InscrEstadualEmpresa { get; set; }

        [JsonProperty("telefoneEmpresa")]
        public string TelefoneEmpresa { get; set; }

        [JsonProperty("enderecoEmpresa")]
        public string EnderecoEmpresa { get; set; }

        [JsonProperty("bairroEmpresa")]
        public string BairroEmpresa { get; set; }

        [JsonProperty("numeroEmpresa")]
        public string NumeroEmpresa { get; set; }

        [JsonProperty("cidadeEmpresa")]
        public string CidadeEmpresa { get; set; }

        [JsonProperty("ufEmpresa")]
        public string UfEmpresa { get; set; }

        [JsonProperty("cepEmpresa")]
        public string CepEmpresa { get; set; }

        [JsonProperty("chaveBpe")]
        public string ChaveBpe { get; set; }

        [JsonProperty("codigoAnttOrigem")]
        public string CodigoAnttOrigem { get; set; }

        [JsonProperty("codigoAnttDestino")]
        public string CodigoAnttDestino { get; set; }

        [JsonProperty("codigoMonitriip")]
        public string CodigoMonitriip { get; set; }

        [JsonProperty("contingencia")]
        public bool Contingencia { get; set; }

        [JsonProperty("dataAutorizacaoBpe")]
        public DateTimeOffset DataAutorizacaoBpe { get; set; }

        [JsonProperty("formaPagamento")]
        public string FormaPagamento { get; set; }

        [JsonProperty("linha")]
        public string Linha { get; set; }

        [JsonProperty("numeroAutorizacaoBpe")]
        public string NumeroAutorizacaoBpe { get; set; }

        [JsonProperty("numeroSistemaBpe")]
        public string NumeroSistemaBpe { get; set; }

        [JsonProperty("outrosTributos")]
        public string OutrosTributos { get; set; }

        [JsonProperty("plataformaEmbarque")]
        public string PlataformaEmbarque { get; set; }

        [JsonProperty("acessoRodoviaria")]
        public string AcessoRodoviaria { get; set; }

        [JsonProperty("precoDesconto")]
        public double PrecoDesconto { get; set; }

        [JsonProperty("precoOutros")]
        public double PrecoOutros { get; set; }

        [JsonProperty("precoPedagio")]
        public double PrecoPedagio { get; set; }

        [JsonProperty("precoSeguroObrigatorio")]
        public double PrecoSeguroObrigatorio { get; set; }

        [JsonProperty("precoSeguroOpcional")]
        public double PrecoSeguroOpcional { get; set; }

        [JsonProperty("precoTarifa")]
        public double PrecoTarifa { get; set; }

        [JsonProperty("precoTaxaEmbarque")]
        public double PrecoTaxaEmbarque { get; set; }

        [JsonProperty("precoTaxaServico")]
        public double PrecoTaxaServico { get; set; }

        [JsonProperty("prefixoLinha")]
        public string PrefixoLinha { get; set; }

        [JsonProperty("protocoloAutorizacao")]
        public string ProtocoloAutorizacao { get; set; }

        [JsonProperty("qrCodeBpe")]
        public Uri QrCodeBpe { get; set; }

        [JsonProperty("serieBpe")]
        public string SerieBpe { get; set; }

        [JsonProperty("tipoDesconto")]
        public string TipoDesconto { get; set; }

        [JsonProperty("troco")]
        public double Troco { get; set; }

        [JsonProperty("valorFormaPagamento")]
        public double ValorFormaPagamento { get; set; }

        [JsonProperty("valorPagar")]
        public double ValorPagar { get; set; }

        [JsonProperty("valorTotal")]
        public double ValorTotal { get; set; }

        [JsonProperty("valorAssento")]
        public object ValorAssento { get; set; }

        [JsonProperty("numeroBilheteAssento")]
        public object NumeroBilheteAssento { get; set; }

        [JsonProperty("moeda")]
        public string Moeda { get; set; }

        [JsonProperty("dabpeByteArray")]
        public string DabpeByteArray { get; set; }
    }
}
