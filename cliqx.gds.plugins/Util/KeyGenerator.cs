using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.Extensions;

namespace cliqx.gds.plugins.Util;

public static class KeyGenerator
{
    public static string Generate(
        string originName
        ,string destinationName
        ,string companyName
        ,DateTime departureTime
        ,DateTime arrivalTime
        ,long value
    )
    {
        var r = $"{originName}-{destinationName}-{companyName}-{departureTime.ToString("HHmmss")}-{arrivalTime.ToString("HHmmss")}-{value}".Trim();
        return r;
    }

    //public static string GenerateCityKey(string cityName, string letterCode) => 
      //  $"{cityName.Trim().ToNormalized()}{letterCode.Trim().ToNormalized()}";

    public static string GenerateCityKey(string cityName, string letterCode)
    {
        var txt = $"{cityName.Trim().ToNormalized()}{letterCode.Trim().ToNormalized()}";
        return txt.Replace(" ", "");
    }
    
}
