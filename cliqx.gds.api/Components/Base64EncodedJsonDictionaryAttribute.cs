using System.ComponentModel.DataAnnotations;
using System.Text;

namespace cliqx.gds.api.Components;

public class Base64EncodedJsonDictionaryAttribute : ValidationAttribute
{
    public Base64EncodedJsonDictionaryAttribute() : base("this property must be a json dictionary<string, string> base64 encoded ")
    {
    }

    public override bool IsValid(object value)
    {
        if (value == null)
            return true;

        if (value.ToString().Equals(""))
            return true;

        try
        {
            var b64b = Convert.FromBase64String(value.ToString());
            var s = Encoding.UTF8.GetString(b64b);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}
