using System.Collections.Generic;
using Nop.Services.Cms;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.SimpleWidget
{
    public class SimpleWidget : BasePlugin, IWidgetPlugin
    {
        public override void Install()
        {
            base.Install();
        }

        public override void Uninstall()
        {
            base.Uninstall();
        }

        public bool HideInWidgetList => false;
        public IList<string> GetWidgetZones()
        {
            return new List<string>{PublicWidgetZones.HomepageBeforeBestSellers};
        }

        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "SimpleWidget";
        }
    }
}