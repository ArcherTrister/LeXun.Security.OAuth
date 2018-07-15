using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Owin.Client.Startup))]
namespace Owin.Client
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
