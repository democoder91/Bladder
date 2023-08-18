using Volo.Abp.Authorization.Permissions;

namespace Bladder.Constants
{
    public class BladderPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var machine = context.AddGroup("Machine");

            machine.AddPermission("Machine_Index");
            machine.AddPermission("Machine_Create");
            machine.AddPermission("Machine_Edit");
            machine.AddPermission("Machine_Show");
            machine.AddPermission("Machine_Delete");

            var bladder = context.AddGroup("Bladder");

            bladder.AddPermission("Bladder_Index");
            bladder.AddPermission("Bladder_Create");
            bladder.AddPermission("Bladder_Edit");
            bladder.AddPermission("Bladder_Show");
            bladder.AddPermission("Bladder_Delete");

            var finding = context.AddGroup("Finding");

            finding.AddPermission("Finding_Index");
            finding.AddPermission("Finding_Create");
            finding.AddPermission("Finding_Edit");
            finding.AddPermission("Finding_Show");
            finding.AddPermission("Finding_Delete");

            var transaction = context.AddGroup("Transaction");

            transaction.AddPermission("Transaction_Index");
            transaction.AddPermission("Transaction_MountCreate");
            transaction.AddPermission("Transaction_DismountCreate");
            transaction.AddPermission("Transaction_MaintenanceCreate");
            transaction.AddPermission("Transaction_TestCreate");

            var bladderSize = context.AddGroup("BladderSize");

            bladderSize.AddPermission("BladderSize_Index");
            bladderSize.AddPermission("BladderSize_Create");
            bladderSize.AddPermission("BladderSize_Edit");
            bladderSize.AddPermission("BladderSize_Show");
            bladderSize.AddPermission("BladderSize_Delete");

            var email = context.AddGroup("Emails");

            email.AddPermission("NotifyForBladderExpiry");
            
        }
    }
}
