using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Services.Plugins;
using Nop.Services.Security;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Widgets.AdminMenuPlugin
{
    public class AdminMenuPlugin : BasePlugin, IAdminMenuPlugin
    {
        private readonly IWebHelper _webHelper;
        private readonly IPermissionService _permissionService;
        public AdminMenuPlugin(IWebHelper webHelper,
            IPermissionService permissionService)
        {
            _webHelper = webHelper;
            _permissionService = permissionService;
        }

        public override async Task InstallAsync()
        {
            await base.InstallAsync();
        }

        public override async Task UninstallAsync()
        {
            await base.UninstallAsync();
        }

        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsAdminMenuPlugin/Configure";
        }
        
        public async Task ManageSiteMapAsync(SiteMapNode rootNode)
        {
            if (await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            {
                var menuItem = new SiteMapNode()
                {
                    Title = "AdminMenuPlugin",
                    Visible = true,
                    IconClass = "far fa-dot-circle",
                    RouteValues = new RouteValueDictionary() {{"area", null}},
                };
                
                var settingsItem = new SiteMapNode()
                {
                    Title = "Configure",
                    ControllerName = "WidgetsAdminMenuPlugin",
                    ActionName = "Configure",
                    Visible = true,
                    IconClass = "far fa-dot-circle",
                    RouteValues = new RouteValueDictionary() { { "area", "admin" } },
                    SystemName = "AdminMenuPlugin.Configure"
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
    }
}