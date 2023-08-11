using Bladder.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Bladder;

public abstract class BladderComponentBase : AbpComponentBase
{
    protected BladderComponentBase()
    {
        LocalizationResource = typeof(BladderResource);
    }
}
