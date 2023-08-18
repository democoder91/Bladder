using Volo.Abp.Domain.Entities;

namespace Bladder.Entities
{
    public class BladderSize : AggregateRoot<int>
    {
        public String Name { get; set; }
    }
}
