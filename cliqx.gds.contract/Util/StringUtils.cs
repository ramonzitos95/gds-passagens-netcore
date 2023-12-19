using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cliqx.gds.contract.Util;

public static class StringUtils
{
    private static readonly Regex diacriticRegex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
    public static string RemoveDiacriticsAndSpecialCharacters(string text)
    {
        string normalizedString = text.Normalize(NormalizationForm.FormD);
        string withoutDiacritics = diacriticRegex.Replace(normalizedString, string.Empty).Normalize(NormalizationForm.FormC);

        int index = withoutDiacritics.IndexOfAny(new char[] { '/', '-', '\\', ':', ',' });
        if (index >= 0)
        {
            withoutDiacritics = withoutDiacritics.Substring(0, index);
        }

        return withoutDiacritics;
    }

    public static string RemoveSpecialCharacters(string text)
    {

        var customText = text;

        int index = customText.IndexOfAny(new char[] { '/', '-', '\\', ':', ',' });
        if (index >= 0)
        {
            customText = customText.Substring(0, index);
        }

        return customText;
    }
}
