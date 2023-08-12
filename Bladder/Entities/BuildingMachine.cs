using Bladder.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Bladder.Entities
{
    public class BuildingMachine  :AggregateRoot<int>
    {
        [Required]
        public string Code { get; set; }
        public List<BuildingBladder> Bladders { get; set; }
    }
}
