using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.Models;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus
{
    public class ValidadorClient
    {
        public static void Validar(Client client)
        {
            if (client == null)
                throw new Exception("O cliente está nulo!");
                  
            if(string.IsNullOrEmpty(client.FullName))
                throw new Exception("O nome do cliente não foi informado!");

            if(!client?.Documents?.Any() ?? false)
                throw new Exception("Não foi informado nenhum documento!");

            // if(client.Documents?.Any() ?? false 
            //     && string.IsNullOrEmpty(client.Documents.FirstOrDefault(x => x.DocumentType == contract.GdsModels.Enum.DocumentTypeEnum.CPF).Value))
            //     throw new Exception("Não foi informado nenhum CPF para o cliente!");

            // if(!client?.Contacts?.Any() ?? false)
            //     throw new Exception("Não foi informado nenhum contato!");

            // if(client.Contacts?.Any() ?? false
            //     && string.IsNullOrEmpty(client.Contacts.FirstOrDefault(x => x.ContactType == contract.GdsModels.Enum.ContactTypeEnum.CelularPessoal).Value))
            //     throw new Exception("Não foi informado nenhum celular para o cliente!");
        }
    }
}