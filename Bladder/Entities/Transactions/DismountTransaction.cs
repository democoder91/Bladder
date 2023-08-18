using Bladder.Services;
using System.ComponentModel.DataAnnotations;

namespace Bladder.Entities.Transactions
{
    public class DismountTransaction : BladderTransaction, IValidatableObject
    {
        public override IEnumerable<ValidationResult> Validate(
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
            }
        }
    }
}
