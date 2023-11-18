using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PAIMANREPOSTERIA.Startup))]
namespace PAIMANREPOSTERIA
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
