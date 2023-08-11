using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Bladder;

[Dependency(ReplaceServices = true)]
public class BladderBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Bladder";
}
