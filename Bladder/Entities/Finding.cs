using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Bladder.Entities
{
    public class Finding :AggregateRoot<int>
    {
        [Required]
        public string IconClass { get; set; }
        public string Description { get; set; }
    }
}
