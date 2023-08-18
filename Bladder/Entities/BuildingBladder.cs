using Bladder.Attributes;
using Bladder.Localization;
using Blazorise;
using Microsoft.Extensions.Localization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Validation.Localization;

namespace Bladder.Entities
{

    public class BuildingBladder  : AggregateRoot<int>, IValidatableObject
    {


        [DisplayName("Bladder Code")]
        public string BladderCode { get; set; }
        [Required]

        public DateTime ExpiryDate { get; set; }
        public string Status { get; set; }
        public int BladderSizeId { get; set; }
        public BladderSize BladderSize { get; set; }

        public bool ExpiryNotificationSent { get; set; } = false;

        public override IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            var localizer = validationContext.GetRequiredService<IStringLocalizer<BladderResource>>();
            if (BladderCode == null)
            {
                yield return new ValidationResult(
                    localizer["this field is required"],
                    new[] { "BladderCode" }
                );
            }
            if (Status ==null)
            {
                yield return new ValidationResult(
                    localizer["this field is required"],
                    new[] { "Status" }
                );
            }
            if (ExpiryDate.Date < DateTime.Now.Date)
            {
                yield return new ValidationResult(
                    localizer["the date has to be today or after"],
                    new[] { "ExpiryDate" }
                );
            }
        }
    }
}
