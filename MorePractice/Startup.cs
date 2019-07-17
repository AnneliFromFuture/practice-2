using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MorePractice.Startup))]
namespace MorePractice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
