using System;
using System.Net.WebSockets;

namespace OpdrachtScenius.Websocket
{
    public interface IWebsocketHandler
    {
        void Handle(Guid id, WebSocket websocket);
    }
}
