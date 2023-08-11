using System.ComponentModel;
using Volo.Abp.Domain.Entities;

namespace Bladder.Entities
{
    public class BuildingMachine  :AggregateRoot<int>
    {
        [DisplayName("Machine Code")]
        public string Code { get; set; }
    }
}
