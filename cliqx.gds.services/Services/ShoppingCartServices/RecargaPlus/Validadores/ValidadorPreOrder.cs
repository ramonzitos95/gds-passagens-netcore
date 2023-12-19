using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.GdsModels;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus
{
    public class ValidadorPreOrder
    {
        public static void Validar(PreOrder preOrder, bool validarItens = true, bool validaPagamento = false, bool validaClient = false)
        {
            if (validaClient)
                ValidadorClient.Validar(preOrder.Client);

            if (validaPagamento)
                ValidadorPayment.Validar(preOrder.Payment);

            // if (validarItens)
            // {
            //     if (preOrder?.Trip?.TotalConnections == 0 && (preOrder?.Seats?.Count() ?? 0) == 0)
            //         throw new Exception("Não foi informado itens para gerar o pedido");

            //     if (preOrder?.Trip?.Connections != null)
            //     {
            //         if (preOrder.Trip.TotalConnections > 0 & !preOrder.Trip.Connections.Any(x => x.Seats.Any()))
            //             throw new Exception("Não foi informado itens para gerar o pedido");
            //     }
            // }

            if (preOrder?.PluginId == Guid.Empty)
                throw new Exception("O plugin não foi informado");
        }
    }
}