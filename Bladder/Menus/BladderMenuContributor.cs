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

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                BladderMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home",
                order: 0
            )
        );
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

        





        if (BladderModule.IsMultiTenant)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        return Task.CompletedTask;
    }
}
