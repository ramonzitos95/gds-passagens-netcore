using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay.Models
{
    public class PagamentoCartaoResponse
    {
        [JsonProperty("message")]
        public string Message { 
            get 
            {
                if (!string.IsNullOrEmpty(_message))
                {

                    // Codificação UTF-8
                    Encoding unicode = Encoding.Unicode;

                    // Convertendo a string para bytes usando UTF-8
                    byte[] utf8Bytes = unicode.GetBytes(_message);

                    // Convertendo os bytes de volta para string usando UTF-8
                    string decodedString = unicode.GetString(utf8Bytes);

                    return decodedString;
                }

                return _message;
            }
            set {
                _message = value;
            }
        }

        private string _message { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("pagamento")]
        public string Pagamento { get; set; }
    }
}
