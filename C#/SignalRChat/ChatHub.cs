using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
namespace SignalRChat
{
    public class ChatHub : Hub
    {
       

        public void Send(string toUserId, string message)
        {
            if (connections.ContainsKey(toUserId))
            {
                string fromUserId = GetMyUserId();
                string connectionId = connections[toUserId];
                Clients.Client(connectionId).receive(fromUserId, message);
            }
        }

        private readonly static Dictionary<string, string> connections = new Dictionary<string, string>();

        public override Task OnConnected()
        {
            string userId = GetMyUserId();

            connections.Add(userId, Context.ConnectionId);

            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            string userId = GetMyUserId();

            connections.Remove(userId);

            return base.OnDisconnected();
        }

        public override Task OnReconnected()
        {
            string userId = GetMyUserId();

            if (connections.ContainsKey(userId))
            {
                connections.Remove(userId);
            }

            connections.Add(userId, Context.ConnectionId);
            
            return base.OnReconnected();
        }

        private string GetMyUserId()
        {
            return HttpContext.Current.Session["UserId"].ToString();
        }
    }
}