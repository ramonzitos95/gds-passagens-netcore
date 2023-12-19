using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using cliqx.gds.contract.Util;

namespace cliqx.gds.contract.GdsModels
{
    public class CustomCity : BaseObjectPlugin
    {

        public string LetterCode { get; set; }
        public string? NormalizedName { get; private set; }
        public string? Station { get; set; }
        [JsonIgnore]
        public List<Component> Components { get; set; }
        private string _name;

        [Required]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NormalizedName = RemoveDiacriticsAndSpecialCharacters(value.ToLower(CultureInfo.InvariantCulture)).Trim();
            }
        }

        private static readonly Regex diacriticRegex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
        private static string RemoveDiacriticsAndSpecialCharacters(string text)
        {
            string normalizedString = text.Normalize(NormalizationForm.FormD);
            string withoutDiacritics = diacriticRegex.Replace(normalizedString, string.Empty).Normalize(NormalizationForm.FormC);

            int index = withoutDiacritics.IndexOfAny(new char[] { '-','/', '\\', ':', ',' });
            if (index >= 0)
            {
                withoutDiacritics = withoutDiacritics.Substring(0, index);
            }

            return withoutDiacritics.Trim();
        }

        public class Component : Packer
        {
            public Guid PluginId { get; set; }
            public string CityId { get; set; }
            public string NormalizedName { get; set; }
        }
    }

}