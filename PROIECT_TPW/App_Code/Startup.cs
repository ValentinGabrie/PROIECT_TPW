using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PROIECT_TPW.Startup))]
namespace PROIECT_TPW
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
