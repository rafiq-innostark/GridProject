using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MSIdentity.Startup))]
namespace MSIdentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
