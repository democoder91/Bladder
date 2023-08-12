using Bladder.Attributes;
using Bladder.Localization;
using Microsoft.Extensions.Localization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Validation.Localization;

namespace Bladder.Entities
{

    public class BuildingBladder  : AggregateRoot<int>
    {


        [Required(ErrorMessage ="this field is required")]
        [DisplayName("Bladder Code")]
        public string BladderCode { get; set; }
        [Required]

        [TodayOrFutureDate(ErrorMessage ="Expiry Date Should Be Today Or Later")]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public string Status { get; set; }
        public int? MachineId { get; set; }
        public BuildingMachine? Machine { get; set; }

    }
}
