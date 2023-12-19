using Newtonsoft.Json;

namespace cliqx.gds.contract.Util;

public abstract class Packer
{
    public string Pack()
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this));
        return System.Convert.ToBase64String(plainTextBytes);
    }

    public static T Unpack<T>(string packed) where T : Packer
    {
        var base64EncodedBytes = System.Convert.FromBase64String(packed);
        return JsonConvert.DeserializeObject<T>(System.Text.Encoding.UTF8.GetString(base64EncodedBytes));
    }
}
