using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CoolerFacebook.Startup))]
namespace CoolerFacebook
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
