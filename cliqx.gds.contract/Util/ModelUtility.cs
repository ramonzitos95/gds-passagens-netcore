using System.Text;
using Newtonsoft.Json;

namespace cliqx.gds.contract.Util;

public static class ModelUtility<T>
{
    public static string Pack(List<T> items)
    {
        var json = JsonConvert.SerializeObject(items);
        var plainTextBytes = Encoding.UTF8.GetBytes(json);
        return Convert.ToBase64String(plainTextBytes);
    }

    public static List<T> Unpack(string packedItems)
    {
        var base64Bytes = Convert.FromBase64String(packedItems);
        var json = Encoding.UTF8.GetString(base64Bytes);
        return JsonConvert.DeserializeObject<List<T>>(json);
    }
}
