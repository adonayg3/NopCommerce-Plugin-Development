using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.ConfigurablePlugin.Models
{
    public record PublicInfoModel : BaseNopModel
    {
        public string ConfigurableText { get; set; }
    }
}