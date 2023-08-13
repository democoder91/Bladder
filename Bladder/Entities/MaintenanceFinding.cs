using Bladder.Entities.Transactions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bladder.Entities
{
    public class MaintenanceFinding
    {

        public int MaintenanceTransactionId { get; set; }
        public MaintenanceTransaction MaintenanceTransaction { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Finding Id Is Required")]
        public int FindingId { get; set; }
        public Finding Finding { get; set; }
        [NotMapped]
        public bool ShowDropdown { get; set; }

        // Add any additional properties if needed
    }
}
