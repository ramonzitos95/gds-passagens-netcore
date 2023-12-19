using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Util
{
    public static class StringUtils
    {
        public static string GenerateKeyFromPedidoItem(ItemPedido iPed)
        {
            return "";
        }

        public static string GenerateKeyFromPreOrderItem(PreOrderItem pItem)
        {
            return "";
        }

        public static long AsLongFromDecimal(this decimal x)
            => Convert.ToInt64(x);


        public static string GetItemPedidoAsString(this ItemPedido item, string chave)
        {
            try
            {
                var ret = item?.Descricoes?.FirstOrDefault(x => x.Chave == chave)?.Valor;

                if (ret is null)
                    return "";

                return ret;
            }
            catch (System.Exception)
            {
                return "";
            }
        }

        public static DateTime GetItemPedidoAsDate(this ItemPedido item, string chave)
        {
            try
            {
                var itm = item.Descricoes?.FirstOrDefault(x => x.Chave == chave)?.Valor;

                if(itm is null)
                    return DateTime.MinValue;

                var ret = DateTime.Parse(itm);

                return ret;

            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        public static Decimal GetItemPedidoAsDecimal(this ItemPedido item, string chave)
        {
            try
            {
                var itm = item?.Descricoes?.FirstOrDefault(x => x.Chave == chave)?.Valor;

                if (itm is null)
                    return 0;

                var ret = Convert.ToDecimal(itm);

                return ret;
            }
            catch (System.Exception)
            {
                return 0;
            }

           
        }

        public static long GetItemPedidoAsLong(this ItemPedido item, string chave)
        {
            try
            {
                var itm = item?.Descricoes.FirstOrDefault(x => x.Chave == chave)?.Valor;

                if (itm is null)
                    return 0;

                var itemDecimal = Convert.ToDecimal(itm) * 100;
                var ret = Convert.ToInt64(itemDecimal);

                return ret;
            }
            catch (System.Exception)
            {
                return 0;
            }
        }

        public static bool GetItemPedidoAsBool(this ItemPedido item, string chave)
        {
            try
            {
                var itm = item.Descricoes?.FirstOrDefault(x => x.Chave == chave)?.Valor;

                if (itm is null)
                    return false;

                var ret = Convert.ToBoolean(itm);

                return ret;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}