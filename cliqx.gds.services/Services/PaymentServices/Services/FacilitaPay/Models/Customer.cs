using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.Models;
using Newtonsoft.Json;

namespace cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay.Models
{
    public class Customer
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("identificationDocument")]
        public string IdentificationDocument { get; set; }

        public Customer(Order order)
        {
            Name = order.Client?.FullName;
            IdentificationDocument = order.Client?.Documents.FirstOrDefault(x => x.DocumentType == contract.GdsModels.Enum.DocumentTypeEnum.CPF).Value;
            
            var address = new Address()
            {
                IsDefault = true,
                City = "São Paulo",
                State = "SP",
                Street = "Rua Luis Coelho",
                ReferencePoint = "",
                Country = "BRASIL",
                ZipCode = "01309001",
                Number = "233",
                Complement = "Escritório, 2º Andar",
                District = "Consolação",
            };
            
            Address = new Address(address);
        } 
    }
}