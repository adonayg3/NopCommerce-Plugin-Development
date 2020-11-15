using System.Linq;
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

        public override void Install()
        {
            base.Install();
        }

        public override void Uninstall()
        {
            base.Uninstall();
        }

        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsAdminMenuPlugin/Configure";
        }
        
        public void ManageSiteMap(SiteMapNode rootNode)
        {
            if (_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
            {
                var menuItem = new SiteMapNode()
                {
                    Title = "AdminMenuPlugin",
                    Visible = true,
                    IconClass = "fa-dot-circle-o",
                    RouteValues = new RouteValueDictionary() {{"area", null}},
                };
                
                var settingsItem = new SiteMapNode()
                {
                    Title = "Configure",
                    ControllerName = "WidgetsAdminMenuPlugin",
                    ActionName = "Configure",
                    Visible = true,
                    IconClass = "fa-dot-circle-o",
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