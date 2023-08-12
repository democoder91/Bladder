using System;
using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class TodayOrFutureDateAttribute : ValidationAttribute
{
    public TodayOrFutureDateAttribute() : base() { }

    public override bool IsValid(object value)
    {
        if (value == null || !(value is DateTime))
        {
            return false;
        }

        DateTime inputDate = (DateTime)value;
        DateTime today = DateTime.Today;

        return inputDate >= today;
    }
}

