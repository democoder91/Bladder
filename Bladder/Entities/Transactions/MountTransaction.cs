using Bladder.Constants;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;

namespace Bladder.Entities.Transactions
{
    public class MountTransaction : BladderTransaction
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage ="Machine Id Is Required")]
        public int MachineId { get; set; }
        public BuildingMachine? Machine { get; set; }  

        // Additional properties specific to Mount transaction
    }
}
