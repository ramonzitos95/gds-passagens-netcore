using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.GdsModels.Query;
using cliqx.gds.contract.Models.PluginConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cliqx.gds.test.ModelsTest
{
    public class GenerateTrip
    {
        public static TripQuery GenerateTripQuery()
        {
            var pluginId = Guid.NewGuid();

            return new TripQuery()
            {
                TravelDate = DateTime.Now.AddDays(20),
                Origin = new CustomCity()
                {
                    LetterCode = "MS",
                    Station = null,
                    Name = "Campo Grande-MS",
                    Id = "1202",
                    ExternalId = Guid.Parse("fc9c0b09-dbbd-4fef-b71b-edc4c0b1ee6b"),
                    ExtraData = "W3siUGx1Z2luSWQiOjIsIkNpdHlJZCI6IkJSRUROIiwiTm9ybWFsaXplZE5hbWUiOiJjYW1wbyBncmFuZGUtbXMifSx7IlBsdWdpbklkIjo1LCJDaXR5SWQiOiIxMjAyIiwiTm9ybWFsaXplZE5hbWUiOiJjYW1wbyBncmFuZGUtbXMifV0=",
                    PluginId = pluginId
                },
                Destination = new CustomCity()
                {
                    LetterCode = "MS",
                    Station = null,
                    Name = "terenos-ms",
                    Id = "2620",
                    ExternalId = Guid.Parse("1fe87e8f-68f6-4787-b03a-667201ac70b2"),
                    ExtraData = "W3siUGx1Z2luSWQiOjUsIkNpdHlJZCI6IjI2MjAiLCJOb3JtYWxpemVkTmFtZSI6InRlcmVub3MtbXMifV0=",
                    PluginId = pluginId
                }
            };
        }

        public static TripQuery GenerateTripQueryWithError()
        {
            var pluginId = Guid.NewGuid();

            return new TripQuery
            {
                TravelDate = DateTime.Now.AddDays(2),
                Origin = new CustomCity()
                {
                    LetterCode = "MS",
                    Station = null,
                    Name = "Campo Grande-MS",
                    Id = "999999",
                    ExternalId = Guid.Parse("fc9c0b09-dbbd-4fef-b71b-edc4c0b1ee6b"),
                    ExtraData = "W3siUGx1Z2luSWQiOjIsIkNpdHlJZCI6IkJSRUROIiwiTm9ybWFsaXplZE5hbWUiOiJjYW1wbyBncmFuZGUtbXMifSx7IlBsdWdpbklkIjo1LCJDaXR5SWQiOiIxMjAyIiwiTm9ybWFsaXplZE5hbWUiOiJjYW1wbyBncmFuZGUtbXMifV0=",
                    PluginId = pluginId
                },
                Destination = new CustomCity()
                {
                    LetterCode = "MS",
                    Station = null,
                    Name = "terenos-ms",
                    Id = "888888",
                    ExternalId = Guid.Parse("1fe87e8f-68f6-4787-b03a-667201ac70b2"),
                    ExtraData = "W3siUGx1Z2luSWQiOjUsIkNpdHlJZCI6IjI2MjAiLCJOb3JtYWxpemVkTmFtZSI6InRlcmVub3MtbXMifV0=",
                    PluginId = pluginId
                }
            };
        }
    }
}
