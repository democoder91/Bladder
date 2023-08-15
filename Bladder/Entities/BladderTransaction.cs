using Bladder.Constants;
using Bladder.Localization;
using Blazorise;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;
using Volo.Abp.Domain.Entities;

namespace Bladder.Entities
{
    public class BladderTransaction : AggregateRoot<int>
    {
        public string TransactionType { get; set; } // For database storage
        

        public int BladderId { get; set; }
        public BuildingBladder? Bladder { get; set; }
        public DateTime CreatedAt { get; set; }

        
    }
}
