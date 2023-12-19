
using Newtonsoft.Json;

namespace cliqx.gds.services.Services.PaymentServices.Services.DefaultPay._Treeal
{

    public partial class Response
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("additionalData")]
        public AdditionalDatum[] AdditionalData { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("base64")]
        public string Base64 { get; set; }

        [JsonProperty("discounts")]
        public object[] Discounts { get; set; }

        [JsonProperty("endToEndId")]
        public object EndToEndId { get; set; }

        [JsonProperty("expirationDate")]
        public object ExpirationDate { get; set; }

        [JsonProperty("expirationSeconds")]
        public long ExpirationSeconds { get; set; }

        [JsonProperty("fineAmount")]
        public object FineAmount { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("interestAmount")]
        public object InterestAmount { get; set; }

        [JsonProperty("maxPaymentDays")]
        public object MaxPaymentDays { get; set; }

        [JsonProperty("modalityAlteration")]
        public bool ModalityAlteration { get; set; }

        [JsonProperty("occurrenceType")]
        public string OccurrenceType { get; set; }

        [JsonProperty("origin")]
        public string Origin { get; set; }

        [JsonProperty("originKey")]
        public object OriginKey { get; set; }

        [JsonProperty("paidAmount")]
        public object PaidAmount { get; set; }

        [JsonProperty("payerDocumentNumber")]
        public string PayerDocumentNumber { get; set; }

        [JsonProperty("payerName")]
        public string PayerName { get; set; }

        [JsonProperty("payerPersonType")]
        public string PayerPersonType { get; set; }

        [JsonProperty("payerRequest")]
        public string PayerRequest { get; set; }

        [JsonProperty("pixKey")]
        public Guid PixKey { get; set; }

        [JsonProperty("pixMessage")]
        public object PixMessage { get; set; }

        [JsonProperty("pixUri")]
        public string PixUri { get; set; }

        [JsonProperty("qrCodeAccurrenceKey")]
        public Guid QrCodeAccurrenceKey { get; set; }

        [JsonProperty("qrCodeKey")]
        public Guid QrCodeKey { get; set; }

        [JsonProperty("qrCodeType")]
        public string QrCodeType { get; set; }

        [JsonProperty("rebateAmount")]
        public object RebateAmount { get; set; }

        [JsonProperty("receiverConciliationId")]
        public string ReceiverConciliationId { get; set; }

        [JsonProperty("sourceAccountBranch")]
        public object SourceAccountBranch { get; set; }

        [JsonProperty("sourceAccountDigit")]
        public object SourceAccountDigit { get; set; }

        [JsonProperty("sourceAccountFinancialInstitution")]
        public object SourceAccountFinancialInstitution { get; set; }

        [JsonProperty("sourceAccountIspb")]
        public object SourceAccountIspb { get; set; }

        [JsonProperty("sourceAccountNumber")]
        public object SourceAccountNumber { get; set; }
    }

}
