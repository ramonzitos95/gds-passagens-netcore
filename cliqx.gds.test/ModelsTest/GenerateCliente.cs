using cliqx.gds.contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cliqx.gds.test.ModelsTest
{
    public class GenerateCliente
    {
        public static Client Generate()
        {
            var documento = new Document() { Value = "95057818066", DocumentType = contract.GdsModels.Enum.DocumentTypeEnum.CPF };
            var documents = new List<Document>() { documento };
            var contatoTelefone = new Contact() { Value = "5511981617830", ContactType = contract.GdsModels.Enum.ContactTypeEnum.CelularPessoal };
            var contatoEmail = new Contact() { Value = "rodrigosilva@gmail.com", ContactType = contract.GdsModels.Enum.ContactTypeEnum.EmailPessoal };
            var contacts = new List<Contact>() { contatoEmail, contatoTelefone };
            return new contract.Models.Client()
            {
                Id = "261",
                Documents = documents,
                FullName = "Rodrigo da Silva",
                Contacts = contacts
            };
        }
    }
}
