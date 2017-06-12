using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AWDAdmin.Startup))]
namespace AWDAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
