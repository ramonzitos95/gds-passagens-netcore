using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models.Response
{
    public class ResultadoOperacao
    {
        public bool Sucesso { get; set; }
        public string Message { get; set; }

        public ResultadoOperacao CriarSucesso(string message)
        {
            return new ResultadoOperacao {  Sucesso = true, Message = message };
        }

        public ResultadoOperacao CriarFalha(string message)
        {
            return new ResultadoOperacao { Sucesso = true, Message = message };
        }
    }
}
