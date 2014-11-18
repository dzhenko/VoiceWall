using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VoiceWall.Web.Startup))]
namespace VoiceWall.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
