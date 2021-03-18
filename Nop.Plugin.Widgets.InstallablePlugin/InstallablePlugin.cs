using System.Threading.Tasks;
using Nop.Services.Plugins;

namespace Nop.Plugin.Widgets.InstallablePlugin
{
    public class InstallablePlugin : BasePlugin
    {
        public override async Task InstallAsync()
        {
            await base.InstallAsync();
        }

        public override async Task UninstallAsync()
        {
            await base.UninstallAsync();
        }
    }
}