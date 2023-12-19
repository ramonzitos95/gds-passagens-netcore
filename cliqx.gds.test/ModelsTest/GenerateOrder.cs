using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.GdsModels.Enum;
using cliqx.gds.contract.Models;

namespace cliqx.gds.test.ModelsTest;

public static class GenerateOrder
{
    public static Order GenerateOrderForPayment()
    {
        var order = new Order
        {
            ExternalId = Guid.NewGuid(),
            TotalValue = 250,
            Id = "83",
            Client = new Client()
            {
                FullName = "Ramon da Silva Santos",
                Documents = new List<Document>()
                        {
                            new Document()
                            {
                                DocumentType = DocumentTypeEnum.CPF,
                                Value = "08390890909"
                            }
                        },
                Contacts = new List<Contact>()
                        {
                            new Contact()
                            {
                                ContactType = ContactTypeEnum.CelularPessoal,
                                Value = "63981311589"
                            }
                        },
                Addresses = new List<Address>() {
                            new Address()
                            {
                                City = "São Paulo",
                                Complement = "APTO 7000",
                                Country = "Brasil",
                                District = "Distrito vilela",
                                Number = "540",
                                ReferencePoint = "Padrão",
                                State = "SP",
                                Street = "Rua São Paulo",
                                ExternalId = Guid.NewGuid(),
                                IsDefault = true
                            }
                        }
            },
            Payment = new Payment()
            {
                Instalments = 1
            }
        };

        return order;
    }
}

