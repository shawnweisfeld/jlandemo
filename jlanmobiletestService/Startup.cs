using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(jlanmobiletestService.Startup))]

namespace jlanmobiletestService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}