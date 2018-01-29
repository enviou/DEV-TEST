using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MuralDeRecados.Startup))]
namespace MuralDeRecados
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
