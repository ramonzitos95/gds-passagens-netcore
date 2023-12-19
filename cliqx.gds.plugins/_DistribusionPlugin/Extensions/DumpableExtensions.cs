using distribusion.api.client.Models.Basic;
using Newtonsoft.Json;

namespace distribusion.api.client.Extensions
{
    public static class DumpableExtensions
    {
        public static string Dump(this IDumpable dumpable)
        {
            return JsonConvert.SerializeObject(dumpable);
        }
    }
}