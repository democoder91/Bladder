using Bladder.Localization;
using Microsoft.Extensions.Localization;

namespace Bladder.Services
{
    public interface ILocalizationServiceCustom
    {
        string localize(string key);
    }

    public class LocalizationServiceCustom : ILocalizationServiceCustom
    {
        private readonly IStringLocalizer<BladderResource> localizer;

        public LocalizationServiceCustom(IStringLocalizer<BladderResource> localizer)
        {
            this.localizer = localizer;
        }
        public string localize(string key)
        {
            return localizer[key];
        }
    }
}
