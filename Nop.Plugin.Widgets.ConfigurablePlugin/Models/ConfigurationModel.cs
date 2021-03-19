using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.ConfigurablePlugin.Models
{
    public record ConfigurationModel : BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }
        public string ConfigurableText { get; set; }
        public bool ConfigurableTextOverrideForStore { get; set; }
    }
}