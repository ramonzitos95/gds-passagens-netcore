using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.GdsModels;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus
{
    public class ValidadorOrder
    {
        public static void Validar(Order order)
        {
            ValidadorClient.Validar(order.Client);

            ValidadorPayment.Validar(order.Payment);

            if (order.Id == null || order.Id == "")
                throw new Exception("Order ID não informado");

            if (!order?.Items?.Any() ?? false)
                throw new Exception("Não foi informado itens para gerar o pedido");

            if (order.PluginId == Guid.Empty)
                throw new Exception("O plugin não foi informado");

            if (string.IsNullOrEmpty(order.Store?.Id))
                throw new Exception($"Não é possível fechar a compra sem dados da loja.");
        }
    }
}