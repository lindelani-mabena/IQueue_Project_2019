using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebServiceSecured.Startup))]

namespace WebServiceSecured
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
