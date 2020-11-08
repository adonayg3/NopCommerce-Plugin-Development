using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.AdminMenuPlugin.Controllers
{
    [Area(AreaNames.Admin)]
    public class WidgetsAdminMenuPluginController : BasePluginController
    {
        public IActionResult Configure()
        {
            return View("~/Plugins/Widgets.AdminMenuPlugin/Views/Configure.cshtml");
        }
    }
}