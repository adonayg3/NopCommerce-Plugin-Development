using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.ConfigurablePlugin.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.ConfigurablePlugin.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class WidgetsConfigurablePluginController : BasePluginController
    {
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;

        public WidgetsConfigurablePluginController(ILocalizationService localizationService,
            INotificationService notificationService,
            IPermissionService permissionService,
            ISettingService settingService,
            IStoreContext storeContext)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _settingService = settingService;
            _storeContext = storeContext;
        }

        public async Task<IActionResult> Configure()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();
            
            var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var configurablePluginSettings = await _settingService.LoadSettingAsync<ConfigurablePluginSettings>(storeScope);
            var model = new ConfigurationModel
            {
                ConfigurableText = configurablePluginSettings.ConfigurableText,
                ActiveStoreScopeConfiguration = storeScope
            };

            if (storeScope > 0)
            {
                model.ConfigurableTextOverrideForStore = await _settingService.SettingExistsAsync(configurablePluginSettings, x => x.ConfigurableText, storeScope);
            }

            return View("~/Plugins/Widgets.ConfigurablePlugin/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Configure(ConfigurationModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();
            
            var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var configurablePluginSettings = await _settingService.LoadSettingAsync<ConfigurablePluginSettings>(storeScope);
            
            configurablePluginSettings.ConfigurableText = model.ConfigurableText;
            
            await _settingService.SaveSettingOverridablePerStoreAsync(configurablePluginSettings, x => x.ConfigurableText, model.ConfigurableTextOverrideForStore, storeScope);
            
            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));
            
            return await Configure();
        }
    }
}