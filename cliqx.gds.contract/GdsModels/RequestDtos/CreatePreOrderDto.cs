using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using cliqx.gds.contract.GdsModels.Enum;

namespace cliqx.gds.contract.GdsModels.RequestDtos
{
    public class CreatePreOrderDto : PreOrderBaseDto
    {
        [Required]
        public Guid PluginId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? ParceiroId { get; set; }

        public IEnumerable<PreOrderItemDto> Items { get; set; }

        public string Channel { get; set; }

    }

    public class PreOrderItemDto
    {
        [Required]
        public TravelDirectionEnum TravelDirectionType { get; set; }

        [Required]
        public Trip Trip { get; set; }
    }


}