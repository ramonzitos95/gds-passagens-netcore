    using System;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
namespace cliqx.gds.plugins._SmartbusPlugin.Models.Response
{

    public partial class ConsultaResponse
    {
        [JsonProperty("data")]
        public List<Datum> Data { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }
    }

    public partial class Datum
    {
        [JsonProperty("nroServico")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long NroServico { get; set; }

        [JsonProperty("servico")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Servico { get; set; }

        [JsonProperty("horaSaida")]
        public DateTimeOffset HoraSaida { get; set; }

        [JsonProperty("horaChegada")]
        public DateTimeOffset HoraChegada { get; set; }

        [JsonProperty("diaChegada")]
        public long DiaChegada { get; set; }

        [JsonProperty("empresaId")]
        public long EmpresaId { get; set; }

        [JsonProperty("nomeEmpresa")]
        public string NomeEmpresa { get; set; }

        [JsonProperty("preco")]
        public double Preco { get; set; }

        [JsonProperty("detalhePreco")]
        public DetalhePreco DetalhePreco { get; set; }

        [JsonProperty("classeServico")]
        public string ClasseServico { get; set; }

        [JsonProperty("bpe")]
        public bool Bpe { get; set; }

        [JsonProperty("conexao")]
        public bool Conexao { get; set; }

        [JsonProperty("assentosLivres")]
        public long AssentosLivres { get; set; }

        [JsonProperty("trechosConexao")]
        public TrechosConexao[] TrechosConexao { get; set; }

        [JsonProperty("moeda")]
        public string Moeda { get; set; }
    }

    public partial class DetalhePreco
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

    public partial class TrechosConexao
    {
        [JsonProperty("nroServico")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long NroServico { get; set; }

        [JsonProperty("servico")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Servico { get; set; }

        [JsonProperty("horaSaida")]
        public DateTimeOffset HoraSaida { get; set; }

        [JsonProperty("horaChegada")]
        public DateTimeOffset HoraChegada { get; set; }

        [JsonProperty("diaChegada")]
        public long DiaChegada { get; set; }

        [JsonProperty("empresaId")]
        public long EmpresaId { get; set; }

        [JsonProperty("nomeEmpresa")]
        public string NomeEmpresa { get; set; }

        [JsonProperty("classeServico")]
        public string ClasseServico { get; set; }

        [JsonProperty("bpe")]
        public bool Bpe { get; set; }

        [JsonProperty("origem")]
        public long Origem { get; set; }

        [JsonProperty("destino")]
        public long Destino { get; set; }
    }

    public partial class ConsultaResponse
    {
        public static ConsultaResponse FromJson(string json) => JsonConvert.DeserializeObject<ConsultaResponse>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this ConsultaResponse self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
