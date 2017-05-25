using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(XamarinHandsOnLabService.Startup))]

namespace XamarinHandsOnLabService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}