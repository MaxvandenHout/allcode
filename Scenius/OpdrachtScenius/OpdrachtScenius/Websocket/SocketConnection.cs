using System;
using System.Net.WebSockets;

namespace OpdrachtScenius.Websocket
{
    public class SocketConnection
    {
        public Guid Id { get; set; }
        public WebSocket WebSocket { get; set; }
    }
}
