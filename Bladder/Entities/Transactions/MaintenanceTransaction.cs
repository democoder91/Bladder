using Bladder.Localization;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;
using Bladder.Constants;
using Blazorise;
using Bladder.Services;

namespace Bladder.Entities.Transactions
{
    public class MaintenanceTransaction : DismountTransaction, IValidatableObject
    {
        [ValidateComplexType]
        public List<MaintenanceFinding> MaintenanceFindings { get; set; } = new List<MaintenanceFinding>();

        // Additional properties specific to Maintenance transaction
        public IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
        {
            var localizer = validationContext.GetRequiredService<ILocalizationServiceCustom>();

            if (localizer is not null)
            {
                if (BladderId == 0)
                {
                    yield return new ValidationResult(
                        localizer.localize("this field is required"),
                        new[] { "BladderId" }
                    );
                }
                foreach (var finding in MaintenanceFindings)
                {
                    finding.Validate(validationContext);
                }
            }

        }
    }

    

    
}
