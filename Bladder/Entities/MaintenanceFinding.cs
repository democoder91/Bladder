using Bladder.Entities.Transactions;
using Bladder.Localization;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bladder.Entities
{
    public class MaintenanceFinding : IValidatableObject
    {

        public int MaintenanceTransactionId { get; set; }
        public MaintenanceTransaction? MaintenanceTransaction { get; set; }

        public int FindingId { get; set; }
        public Finding? Finding { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var localizer = validationContext.GetRequiredService<IStringLocalizer<BladderResource>>();

            if (FindingId == 0 )
            {
                yield return new ValidationResult(
                    localizer["this field is required"],
                    new[] { "FindingId" }
                );
            }
        }

        // Add any additional properties if needed
    }
}
