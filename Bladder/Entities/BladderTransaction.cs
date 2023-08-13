using Bladder.Constants;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Users;

namespace Bladder.Entities
{
    public class BladderTransaction : AggregateRoot<int>
    {
        public string TransactionType { get; set; } // For database storage
        [Range(1, int.MaxValue, ErrorMessage = "Bladder Id Is Required")]

        public int BladderId { get; set; }
        public BuildingBladder? Bladder { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
