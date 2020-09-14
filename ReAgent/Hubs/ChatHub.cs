using Microsoft.AspNet.SignalR;
using Microsoft.Practices.Unity;
using ReAgent.Services.API;

namespace ReAgent.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string chatId, string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(chatId, name, message);
            App_Start.UnityConfig.ResolveDependency<IOrderServ>().SaveChatMsg(chatId, name, message); // primer Dependency Injection ispol'zovanie
        }
    }
}