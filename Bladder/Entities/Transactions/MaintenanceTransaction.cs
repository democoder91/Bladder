using System.ComponentModel.DataAnnotations;

namespace Bladder.Entities.Transactions
{
    public class MaintenanceTransaction : BladderTransaction
    {
        [Required]
        [EnsureMinimumElements(1, ErrorMessage = "At least one maintenance finding is required.")]
        [EnsureUniqueFindingIds(ErrorMessage = "Duplicate Findings.")]
        public List<MaintenanceFinding> MaintenanceFindings { get; set; } = new List<MaintenanceFinding>();

        // Additional properties specific to Maintenance transaction
    }

    public class EnsureMinimumElementsAttribute : ValidationAttribute
    {
        private readonly int _minimumElements;

        public EnsureMinimumElementsAttribute(int minimumElements)
        {
            _minimumElements = minimumElements;
        }

        public override bool IsValid(object? value)
        {
            var list = value as IList<MaintenanceFinding>;
            return list != null && list.Count >= 0;
        }
    }

    public class EnsureUniqueFindingIdsAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var findings = value as List<MaintenanceFinding>;
            if (findings == null)
            {
                return true; // Not applicable, let Required attribute handle this
            }

            return findings.GroupBy(f => f.FindingId).All(g => g.Count() == 1);
        }
    }
}
