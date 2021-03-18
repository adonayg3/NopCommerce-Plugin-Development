using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.AdminMenuPlugin.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class WidgetsAdminMenuPluginController : BasePluginController
    {
        public IActionResult Configure()
        {
            return View("~/Plugins/Widgets.AdminMenuPlugin/Views/Configure.cshtml");
        }
    }
}