using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using cliqx.gds.contract.Util;

namespace cliqx.gds.contract.Extensions
{
    public static class StringExtensions
    {
        public static string ToNormalized(this string text)
        {
            return StringUtils.RemoveDiacriticsAndSpecialCharacters(text);
        }

        public static string ToNormalizedWithAccents(this string text)
        {
            return StringUtils.RemoveSpecialCharacters(text);
        }

    }
}