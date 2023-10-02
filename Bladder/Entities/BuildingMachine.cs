using Bladder.Attributes;
using Bladder.Localization;
using Microsoft.Extensions.Localization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Bladder.Entities
{
    public class BuildingMachine  :AggregateRoot<int>, IValidatableObject
    {
        //[Required]
        public string Code { get; set; }
        public bool Full { get; set; }
        public int? LeftBladderId { get; set; }
        public BuildingBladder? LeftBladder { get; set; }

        public int? RightBladderId { get; set; }
        public BuildingBladder? RightBladder { get; set; }
        public override IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            var localizer = validationContext.GetRequiredService<IStringLocalizer<BladderResource>>();
            if (Code  == null)
            {
                yield return new ValidationResult(
                    localizer["this field is required"],
                    new[] { "Code"}
                );
            }
        }
    }
}
