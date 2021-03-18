using System.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.ConfigurablePlugin.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.ConfigurablePlugin.Components
{
    [ViewComponent(Name = "WidgetsConfigurablePlugin")]
    public class WidgetsConfigurablePluginViewComponent : NopViewComponent
    {
        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingService;

        public WidgetsConfigurablePluginViewComponent(IStoreContext storeContext, ISettingService settingService)
        {
            _storeContext = storeContext;
            _settingService = settingService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            var configurablePluginSettings = await _settingService.LoadSettingAsync<ConfigurablePluginSettings>((await _storeContext.GetCurrentStoreAsync()).Id);;

            var model = new PublicInfoModel
            {
                ConfigurableText = configurablePluginSettings.ConfigurableText
            };
            
            return View("~/Plugins/Widgets.ConfigurablePlugin/Views/PublicInfo.cshtml", model);
        }
    }
}
