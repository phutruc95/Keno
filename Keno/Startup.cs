using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Keno.Startup))]
namespace Keno
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
