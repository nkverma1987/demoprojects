using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCDemoNew.Startup))]
namespace MVCDemoNew
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
