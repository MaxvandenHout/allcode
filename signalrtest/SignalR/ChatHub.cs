using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace SignalRTest.SignalR
{
    public class ChatHub : Hub
    {
        private static Dictionary<string, string> connections = new Dictionary<string, string>();
        private static object lockObject = new object();

        public void WriteMessage(string name, string message)
        {
            var myName = UserSession.CurrentFromContext(Context)["Name"].ToString();
            if (name.Trim().ToLower() == "all")
            {
                Clients.All.sendMessage(myName, message);
            }
            else
            {
                string connectionId = null;
                lock (lockObject)
                {
                    if (connections.ContainsKey(name))
                    {
                        connectionId = connections[name];        
                    }
                }
                if (!string.IsNullOrWhiteSpace(connectionId))
                {
                    Clients.Client(connectionId).sendMessage(myName, message);
                }
            }
        }

        public override Task OnConnected()
        {
            RegisterConnection();
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            RegisterConnection();
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var name = UserSession.CurrentFromContext(Context)["Name"].ToString();
            lock (lockObject)
            {
                UnregisterConnection(name);
            }
            return base.OnDisconnected(stopCalled);
        }

        private void RegisterConnection()
        {
            var name = UserSession.CurrentFromContext(Context)["Name"].ToString();
            lock (lockObject)
            {
                UnregisterConnection(name);
                foreach (var connection in connections)
                {
                    Clients.Caller.userConnected(connection.Value, connection.Key);
                }
                connections[name] = Context.ConnectionId;
            }
            Clients.All.userConnected(Context.ConnectionId, name);
        }

        private void UnregisterConnection(string name)
        {
            if (connections.ContainsKey(name))
            {
                var connectionId = connections[name];
                connections.Remove(name);
                Clients.All.userDisconnected(connectionId);
            }
        }
    }
}