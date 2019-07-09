using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GreenvilleFarmsProject.Startup))]
namespace GreenvilleFarmsProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
