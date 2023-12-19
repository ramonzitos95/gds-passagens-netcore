using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cliqx.gds.services.Services.PaymentServices.Services.DefaultPay._Treeal
{

    public partial class Request
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("qrCodeType")]
        public string QrCodeType { get; set; }

        [JsonProperty("expirationSeconds")]
        public long ExpirationSeconds { get; set; }

        [JsonProperty("pixKey")]
        public string PixKey { get; set; }

        [JsonProperty("payerName")]
        public string PayerName { get; set; }

        [JsonProperty("payerDocumentNumber")]
        public string PayerDocumentNumber { get; set; }

        [JsonProperty("payerPersonType")]
        public string PayerPersonType { get; set; }

        [JsonProperty("payerRequest")]
        public string PayerRequest { get; set; }

        [JsonProperty("additionalData")]
        public List<AdditionalDatum> AdditionalData { get; set; }

        [JsonProperty("urlReturn")]
        public string UrlReturn { get; set; }
    }

    public partial class AdditionalDatum
    {
        [JsonProperty("key_name")]
        public string KeyName { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

}

