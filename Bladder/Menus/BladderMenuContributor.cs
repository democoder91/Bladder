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
        
        context.Menu.Items.Insert(
        1,
        new ApplicationMenuItem(
            BladderMenus.Machines,
            l["Menu:Machines"],
            "/Machines/Index",
            icon: "fa fa-cogs",
            order: 1 ,
            null,
            null,
            null,
            null,
            "Machine_Index"
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
