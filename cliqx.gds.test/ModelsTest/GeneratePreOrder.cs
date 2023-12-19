using cliqx.gds.contract.Enums;
using cliqx.gds.contract.GdsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cliqx.gds.test.ModelsTest
{
    public class GeneratePreOrder
    {
        public static PreOrder GenerateWithSuccess()
        {
            return new PreOrder()
            {
                Id = "83",
                Status = StatusPedido.PRE_PEDIDO.ToString(),
                ExternalId = Guid.Parse("2eedc48c-1f28-48c3-8b67-696e9edef07f"),
                PluginId = Guid.NewGuid(),
                StoreId = "2",
                Store = new Store()
                {
                    Id = "2",
                    Name = "Loja"
                },
                Client = GenerateCliente.Generate()
            };
        }
    }
}
