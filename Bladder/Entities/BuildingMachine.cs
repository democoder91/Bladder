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
        public List<BuildingBladder> Bladders { get; set; }
        public bool Full { get; set; }
        public IEnumerable<ValidationResult> Validate(
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
