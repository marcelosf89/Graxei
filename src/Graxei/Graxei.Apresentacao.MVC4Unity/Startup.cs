using Owin;
using Microsoft.Owin;
[assembly: OwinStartup(typeof(Graxei.Apresentacao.MVC4Unity.Startup))]
namespace Graxei.Apresentacao.MVC4Unity
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}