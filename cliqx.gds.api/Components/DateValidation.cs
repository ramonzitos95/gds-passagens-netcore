using System.ComponentModel.DataAnnotations;
using System.Text;

namespace cliqx.gds.api.Components;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class DateValidation : ValidationAttribute
{
    private readonly DateTime _defaultDate;

    public DateValidation()
    {
        _defaultDate = new DateTime(1900, 1, 1);
    }

    public override bool IsValid(object value)
    {
        if (value is DateTime date && date == _defaultDate)
        {
            return false;
        }

        return true;
    }
}
