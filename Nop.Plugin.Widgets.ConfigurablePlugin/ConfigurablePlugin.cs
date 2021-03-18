using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Services.Security;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Widgets.ConfigurablePlugin
{
    public class ConfigurablePlugin : BasePlugin, IWidgetPlugin, IAdminMenuPlugin
    {
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;
        private readonly IPermissionService _permissionService;

        public ConfigurablePlugin(ILocalizationService localizationService,
            ISettingService settingService,
            IWebHelper webHelper,
            IPermissionService permissionService)
        {
            _localizationService = localizationService;
            _settingService = settingService;
            _webHelper = webHelper;
            _permissionService = permissionService;
        }
        
        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string>{PublicWidgetZones.HomepageBeforeBestSellers});
        }
        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsConfigurablePlugin/Configure";
        }
        
        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "WidgetsConfigurablePlugin";
        }
        
        public override async Task InstallAsync()
        {
            var settings = new ConfigurablePluginSettings
            {
                ConfigurableText = "Edit Text in admin section"
            };
            await _settingService.SaveSettingAsync(settings);

            await _localizationService.AddLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Plugins.Widgets.ConfigurablePlugin.Text"] = "ConfigurablePlugin",
            });

            await base.InstallAsync();
        }
        
        public override async Task UninstallAsync()
        {
            await _settingService.DeleteSettingAsync<ConfigurablePluginSettings>();
            
            await _localizationService.DeleteLocaleResourcesAsync("Plugins.Widgets.ConfigurablePlugin");

            await base.UninstallAsync();
        }

        public async Task ManageSiteMapAsync(SiteMapNode rootNode)
        {
            if (await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            {
                var menuItem = new SiteMapNode()
                {
                    Title = "ConfigurablePlugin",
                    Visible = true,
                    IconClass = "fa fa-cogs",
                    RouteValues = new RouteValueDictionary() {{"area", null}},
                };
                
                var settingsItem = new SiteMapNode()
                {
                    Title = "Configure",
                    ControllerName = "WidgetsConfigurablePlugin",
                    ActionName = "Configure",
                    Visible = true,
                    IconClass = "far fa-dot-circle",
                    RouteValues = new RouteValueDictionary() { { "area", "admin" } },
                    SystemName = "ConfigurablePlugin.Configure"
                };
                menuItem.ChildNodes.Add(settingsItem);

                var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "CustomPlugins");

                if (pluginNode != null)
                    pluginNode.ChildNodes.Add(menuItem);
                else
                {
                    var customPluginNode = new SiteMapNode()
                    {
                        Visible = true,
                        Title = "CustomPlugins",
                        Url = "",
                        SystemName = "CustomPlugins",
                        IconClass = "fa fas fa-plug"
                    };
                    rootNode.ChildNodes.Add(customPluginNode);
                    customPluginNode.ChildNodes.Add(menuItem);
                }
            }
        }
        public bool HideInWidgetList => false;
    }
}