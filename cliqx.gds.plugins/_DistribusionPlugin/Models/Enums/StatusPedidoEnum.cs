using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cliqx.gds.plugins._DistribusionPlugin.Models.Enums
{
    public static class StatusPedidoEnum
    {
        public const int PrePedido = 1;
        public const int AguardandoPagamento = 2;
        public const int ConfirmadoPagamento = 3;
        public const int Entregue = 4;
        public const int Cancelado = 9999;
    }
}