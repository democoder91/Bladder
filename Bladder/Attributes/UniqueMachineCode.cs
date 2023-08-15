using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Bladder.Data; // Import the appropriate namespace for your DbContext
using Bladder.Entities;

namespace Bladder.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false)]
    public class UniqueMachineCodeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string machineCode && validationContext.ObjectInstance is BuildingMachine machine)
            {
                var dbContext = validationContext.GetService(typeof(BladderDbContext)) as BladderDbContext;

                if (dbContext != null)
                {
                    var existingMachine = dbContext.Set<BuildingMachine>()
                        .FirstOrDefault(b => b.Id != machine.Id && b.Code == machineCode);

                    if (existingMachine == null)
                    {
                        return ValidationResult.Success;
                    }
                }

                return new ValidationResult(ErrorMessage);
            }

            throw new InvalidOperationException("This attribute is intended to be used with string properties on BuildingMachine class.");
        }
    }
}
