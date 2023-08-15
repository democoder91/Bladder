using Bladder.Localization;
using Volo.Abp.Identity.Blazor;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.TenantManagement.Blazor.Navigation;
using Volo.Abp.UI.Navigation;

namespace Bladder.Menus;

public class BladderMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<BladderResource>();

        
        context.Menu.AddGroup(
                new ApplicationMenuGroup(
                    name: "MasterData",
                    displayName: l["Master Data"]
                )
            );
        context.Menu.AddItem(
                new ApplicationMenuItem("Bladder", l["Master Data"], groupName: "MasterData")
                    .AddItem(new ApplicationMenuItem(
                        name: "Bladder.Machines",
                        displayName: l["Menu:Machines"],
                        url: "/Machines/Index",
                        requiredPermissionName: "Machine_Index"
                        )
                    )
                    .AddItem(new ApplicationMenuItem(
                        name: "Bladder.Bladders",
                        displayName: l["Menu:Bladders"],
                        url: "/Bladders/Index",
                        requiredPermissionName: "Bladder_Index"
                        )
                    )
                    .AddItem(new ApplicationMenuItem(
                        name: "Bladder.Findings",
                        displayName: l["Menu:Findings"],
                        url: "/Findings/Index",
                        requiredPermissionName: "Finding_Index"
                        )
                    )

            );
        context.Menu.Items.Insert(
            2,
            new ApplicationMenuItem(
                BladderMenus.BladderTransactions,
                l["Menu:BladderTransactions"],
                "/Transactions/Index",
                icon: "fas fa-home",
                requiredPermissionName:"Transaction_Index"
            )
        );






        administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);

        //if (BladderModule.IsMultiTenant)
        //{
        //    administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        //}
        //else
        //{
        //}

        return Task.CompletedTask;
    }
}
