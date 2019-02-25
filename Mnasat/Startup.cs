using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mnasat.Startup))]
namespace Mnasat
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
