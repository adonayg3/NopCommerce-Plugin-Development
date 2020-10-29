using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.SimpleWidget.Components
{
    [ViewComponent(Name = "SimpleWidget")]
    public class SimpleWidgetViewComponent : NopViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Plugins/Widgets.SimpleWidget/Views/SimpleWidgetView.cshtml");
        }
    }
}