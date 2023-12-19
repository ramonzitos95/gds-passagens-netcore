using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using cliqx.gds.contract.Models;

namespace cliqx.gds.contract.GdsModels.RequestDtos
{
    public class PreOrderBaseDto
    {
        public Client Client { get; set; }
        
        /// <summary>
        /// Token do parceiro, vinculo da empresa com o pedido
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string TokenParceiro { get; set; }
    }
}