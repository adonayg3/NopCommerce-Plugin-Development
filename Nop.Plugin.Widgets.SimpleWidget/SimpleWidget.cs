using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Services.Cms;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.SimpleWidget
{
    public class SimpleWidget : BasePlugin, IWidgetPlugin
    {
        public override async Task InstallAsync()
        {
            await base.InstallAsync();
        }

        public override async Task UninstallAsync()
        {
            await base.UninstallAsync();
        }

        public bool HideInWidgetList => false;
        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string>{PublicWidgetZones.HomepageBeforeBestSellers});
        }

        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "SimpleWidget";
        }
    }
}