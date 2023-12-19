using Newtonsoft.Json;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Models.Response
{
    public class OrgaoConcedente
    {
        [JsonProperty("orgaoConcedenteId")]
        public int OrgaoConcedenteId { get; set; }

        [JsonProperty("descOrgao")]
        public string DescOrgao { get; set; }

        [JsonProperty("idadeMinimaIdoso")]
        public int IdadeMinimaIdoso { get; set; }

        [JsonProperty("idadeMaximaCrianca")]
        public int IdadeMaximaCrianca { get; set; }
    }
}
