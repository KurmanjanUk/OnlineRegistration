using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineRegistration.Startup))]
namespace OnlineRegistration
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
