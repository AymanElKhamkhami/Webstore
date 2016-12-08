using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UIP_Webstore.Startup))]
namespace UIP_Webstore
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
