using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SSISWebApplication.Startup))]
namespace SSISWebApplication
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
