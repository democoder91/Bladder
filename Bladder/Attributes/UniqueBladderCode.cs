using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Bladder.Data; // Import the appropriate namespace for your DbContext
using Bladder.Entities;

namespace Bladder.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class UniqueBladderCodeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string bladderCode && validationContext.ObjectInstance is BuildingBladder bladder)
            {
                var dbContext = validationContext.GetService(typeof(BladderDbContext)) as BladderDbContext;

                if (dbContext != null)
                {
                    var existingBladder = dbContext.Set<BuildingBladder>()
                        .FirstOrDefault(b => b.Id != bladder.Id && b.BladderCode == bladderCode);

                    if (existingBladder == null)
                    {
                        return ValidationResult.Success;
                    }
                }

                return new ValidationResult(ErrorMessage);
            }

            throw new InvalidOperationException("This attribute is intended to be used with string properties on BuildingBladder class.");
        }
    }
}