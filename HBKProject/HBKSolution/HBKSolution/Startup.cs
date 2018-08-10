using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HBKSolution.Startup))]
namespace HBKSolution
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
