using Bladder.Constants;
using Bladder.Localization;
using Blazorise;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;

namespace Bladder.Entities.Transactions
{
    public class MountTransaction : BladderTransaction, IValidatableObject
    {
        //[Required]
        //[Range(1, int.MaxValue, ErrorMessage ="Machine Id Is Required")]
        public int MachineId { get; set; }
        public BuildingMachine? Machine { get; set; }

        // Additional properties specific to Mount transaction

        public IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
        {
            var localizer = validationContext.GetRequiredService<IStringLocalizer<BladderResource>>();
            if (localizer is not null)
            {
                if (MachineId == 0)
                {
                    yield return new ValidationResult(
                        localizer["this field is required"],
                        new[] { "MachineId" }
                    );
                }
                if (BladderId == 0)
                {
                    yield return new ValidationResult(
                        localizer["this field is required"],
                        new[] { "BladderId" }
                    );
                }
            }

        }
    }
}
