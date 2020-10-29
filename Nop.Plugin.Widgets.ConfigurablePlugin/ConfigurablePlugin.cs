using System.Collections.Generic;
using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.ConfigurablePlugin
{
    public class ConfigurablePlugin : BasePlugin, IWidgetPlugin
    {
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;

        public ConfigurablePlugin(ILocalizationService localizationService,
            ISettingService settingService,
            IWebHelper webHelper)
        {
            _localizationService = localizationService;
            _settingService = settingService;
            _webHelper = webHelper;
        }
        
        public IList<string> GetWidgetZones()
        {
            return new List<string>{PublicWidgetZones.HomepageBeforeBestSellers};
        }
        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsConfigurablePlugin/Configure";
        }
        
        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "WidgetsConfigurablePlugin";
        }
        
        public override void Install()
        {
            var settings = new ConfigurablePluginSettings
            {
                ConfigurableText = "Edit Text in admin section"
            };
            _settingService.SaveSetting(settings);

            _localizationService.AddPluginLocaleResource(new Dictionary<string, string>
            {
                ["Plugins.Widgets.ConfigurablePlugin.Text"] = "ConfigurablePlugin",
            });

            base.Install();
        }
        
        public override void Uninstall()
        {
            _settingService.DeleteSetting<ConfigurablePluginSettings>();
            
            _localizationService.DeletePluginLocaleResources("Plugins.Widgets.ConfigurablePlugin");

            base.Uninstall();
        }
        
        public bool HideInWidgetList => false;
    }
}