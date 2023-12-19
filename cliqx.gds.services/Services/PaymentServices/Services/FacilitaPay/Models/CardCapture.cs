using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.Models.PluginConfigurations;
using Newtonsoft.Json;

namespace cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay.Models
{
    public class CardCapture
    {
         public class Resquest
        {
            [JsonProperty("token")]
            public string Token { get; set; }

            [JsonProperty("value")]
            public decimal Value { get; set; }

            [JsonProperty("phone")]
            public string Phone { get; set; }
            
            [JsonProperty("guid")]
            public string Guid { get; set; }
            
            [JsonProperty("extraData")]
            public string ExtraData { get; set; }
            
            [JsonProperty("splits")]
            public string Splits { get; set; }
            
            [JsonProperty("payType")]
            public string PayType { get; set; }

            [JsonProperty("juros")]
            public decimal Juros { get; set; }

            [JsonProperty("bill")]
            public Bill Bill { get; set; }

            [JsonProperty("itens")]
            public List<Item> Itens{ get; set; }

            [JsonProperty("urlCallback")]
            public string UrlCallback { get; set; }

            [JsonProperty("pixKey")]
            public string PixKey { get; set; }

            public Resquest(string token,  Order order, string modo = "CAPTURA", List<Item> itens = null)
            {
               Token = token;
               Value = order.TotalValueAsDecimal;
               Phone = order.Client.Contacts.FirstOrDefault(x => x.ContactType == contract.GdsModels.Enum.ContactTypeEnum.CelularPessoal).Value;
               Guid = order.Id.ToString();
               ExtraData = order.ExtraData;
               Splits = (order.Payment?.Instalments ?? 0) < 1 ? "1" : $"{order.Payment?.Instalments}";
               PayType = modo;
               Itens = itens == null ? new List<Item>() : itens;
               Bill = new Bill(order);
            }
        }

        public class Response
        {
            public string Status { get; set; }
            public string Uuid { get; set; }
            public string Link { get; set; }
            public string Cod { get; set; }
            public string Message { get; set; }
        }
    }
}