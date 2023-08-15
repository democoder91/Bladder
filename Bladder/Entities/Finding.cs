using Bladder.Localization;
using Blazorise;
using Microsoft.Extensions.Localization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Bladder.Entities
{
    public class Finding :AggregateRoot<int>,IValidatableObject
    {
        public string IconClass { get; set; }
        public string Description { get; set; }

        public List<MaintenanceFinding> MaintenanceFindings { get; set; } = new List<MaintenanceFinding>();

        public IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
        {
            var localizer = validationContext.GetRequiredService<IStringLocalizer<BladderResource>>();
            if (IconClass == null)
            {
                yield return new ValidationResult(
                    localizer["this field is required"],
                    new[] { "IconClass" }
                );
            }
            if (Description == null)
            {
                yield return new ValidationResult(
                    localizer["this field is required"],
                    new[] { "Description" }
                );
            }
        }
    }
}
