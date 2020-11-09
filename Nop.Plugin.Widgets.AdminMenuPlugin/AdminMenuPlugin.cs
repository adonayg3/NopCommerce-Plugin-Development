using System.Linq;
using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Services.Plugins;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Widgets.AdminMenuPlugin
{
    public class AdminMenuPlugin : BasePlugin, IAdminMenuPlugin
    {
        private readonly IWebHelper _webHelper;
        private readonly IPermissionService _permissionService;

        public AdminMenuPlugin(IWebHelper webHelper)
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
                    IconClass = "fa fas fa-align-justify",
                    RouteValues = new RouteValueDictionary() {{"area", null}},
                };

                var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "CustomPlugins");

                if (pluginNode != null)
                    pluginNode.ChildNodes.Add(menuItem);
                else
                {
                    var configurablePluginNode = new SiteMapNode()
                    {
                        Visible = true,
                        Title = "CustomPlugins",
                        Url = "",
                        SystemName = "CustomPlugins",
                        IconClass = "fa fas fa-plug"
                    };
                    rootNode.ChildNodes.Add(configurablePluginNode);
                    configurablePluginNode.ChildNodes.Add(menuItem);
                }
            }
        }
    }
}