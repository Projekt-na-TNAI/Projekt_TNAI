using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TNAI.MVC.Startup))]
namespace TNAI.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
