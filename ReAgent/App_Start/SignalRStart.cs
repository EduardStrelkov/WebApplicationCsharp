using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(ReAgent.App_Start.SignalRStart))]
namespace ReAgent.App_Start
{
    public class SignalRStart
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}