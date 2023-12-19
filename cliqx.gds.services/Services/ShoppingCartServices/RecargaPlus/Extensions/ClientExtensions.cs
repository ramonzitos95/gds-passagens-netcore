
using cliqx.gds.contract.GdsModels.Enum;
using cliqx.gds.contract.Models;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models.Request;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models.Response;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Extensions
{
    public static class ClientExtensions
    {
        public static contract.Models.Client AsGdsClient(this ClientResponse client)
        => new contract.Models.Client
        {
            Id = client.Id.ToString()
            ,FullName = client?.NomeCompleto
            ,IsActive = client?.Ativo == "S" ?  true : false
            ,Documents = new List<Document>(){new Document {DocumentType = DocumentTypeEnum.CPF, Value = client?.Cpf }}
            ,Contacts = new List<Contact>(){
                new Contact{ContactType = ContactTypeEnum.EmailPessoal, Value = client?.Contato?.Email}
                ,new Contact{ContactType = ContactTypeEnum.CelularPessoal, Value = client?.Contato?.Telefone}
                }
        };

        public static ClientRequest AsRecargaClient(this contract.Models.Client client, long lojaId)
        => new ClientRequest
        {
            Id = int.Parse(client.Id)
            ,Nome = client.FullName.Split(" ").First()
            ,Sobrenome = client.FullName.Split(" ").Last()
            ,Cpf = client.Documents.FirstOrDefault(x => x.DocumentType == contract.GdsModels.Enum.DocumentTypeEnum.CPF).Value
            ,Email = client.Contacts.FirstOrDefault(x => x.ContactType == contract.GdsModels.Enum.ContactTypeEnum.EmailPessoal).Value
            ,Telefone = client.Contacts.FirstOrDefault(x => x.ContactType == contract.GdsModels.Enum.ContactTypeEnum.CelularPessoal).Value
            ,LojaId = lojaId
        };

        
    }
}