using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models.Response
{
    public class ParceiroResponse
    {
        public long Id { get; set; }
        public string Nome { get; set; }

        public decimal? TaxaServico { get; set; }

        public string Token { get; set; }
    }
}
