
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using cliqx.gds.contract.Models.Global;
using cliqx.gds.contract.Models.PluginConfigurations;

namespace cliqx.gds.contract.Models;

public partial class City : BasicObjectWithPlugin
{

    public string LetterCode { get; set; }

    [QueryOperator(Operator = WhereOperator.Contains)]
    public string? NormalizedName { get; private set; }
    public string? Description { get; set; }
    private string _name;



    [QueryOperator(Operator = WhereOperator.Contains)]
    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            NormalizedName = RemoveDiacriticsAndSpecialCharacters(value.ToLower(CultureInfo.InvariantCulture));
        }
    }

    private static readonly Regex diacriticRegex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
    private static string RemoveDiacriticsAndSpecialCharacters(string text)
    {
        string normalizedString = text.Normalize(NormalizationForm.FormD);
        string withoutDiacritics = diacriticRegex.Replace(normalizedString, string.Empty).Normalize(NormalizationForm.FormC);

        int index = withoutDiacritics.IndexOfAny(new char[] { '/', '-', '\\', ':' });
        if (index >= 0)
        {
            withoutDiacritics = withoutDiacritics.Substring(0, index);
        }

        return withoutDiacritics;
    }
}