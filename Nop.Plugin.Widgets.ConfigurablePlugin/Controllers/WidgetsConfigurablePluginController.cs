using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.ConfigurablePlugin.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.ConfigurablePlugin.Controllers
{
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

        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();
            
            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var configurablePluginSettings = _settingService.LoadSetting<ConfigurablePluginSettings>(storeScope);
            var model = new ConfigurationModel
            {
                ConfigurableText = configurablePluginSettings.ConfigurableText,
                ActiveStoreScopeConfiguration = storeScope
            };

            if (storeScope > 0)
            {
                model.ConfigurableTextOverrideForStore = _settingService.SettingExists(configurablePluginSettings, x => x.ConfigurableText, storeScope);
            }

            return View("~/Plugins/Widgets.ConfigurablePlugin/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();
            
            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var configurablePluginSettings = _settingService.LoadSetting<ConfigurablePluginSettings>(storeScope);
            
            configurablePluginSettings.ConfigurableText = model.ConfigurableText;
            
            _settingService.SaveSettingOverridablePerStore(configurablePluginSettings, x => x.ConfigurableText, model.ConfigurableTextOverrideForStore, storeScope);
            
            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));
            
            return Configure();
        }
    }
}