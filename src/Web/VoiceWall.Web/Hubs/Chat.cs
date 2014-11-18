namespace VoiceWall.Web.Hubs
{
    using Microsoft.AspNet.SignalR;

    public class Chat : Hub
    {
        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }
    }
}