using Volo.Abp.Authorization.Permissions;

namespace Bladder.Constants
{
    public class BladderPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup("Machine");

            myGroup.AddPermission("Machine_Index");
            myGroup.AddPermission("Machine_Create");
            myGroup.AddPermission("Machine_Edit");
            myGroup.AddPermission("Machine_Show");
            myGroup.AddPermission("Machine_Delete");
        }
    }
}
