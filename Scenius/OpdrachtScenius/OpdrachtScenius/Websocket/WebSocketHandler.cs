using OpdrachtScenius.Models;
using OpdrachtScenius.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace OpdrachtScenius.Websocket
{
    public class WebsocketHandler : IWebsocketHandler
    {
        public List<SocketConnection> websocketConnections = new List<SocketConnection>();

        private readonly IQueueHandler queueHandler;
        public WebsocketHandler(IQueueHandler queueHandler)
        {
            this.queueHandler = queueHandler;
            this.queueHandler.MessageRecieved += QueueHandler_MessageRecieved;
            SetupCleanUpTask();
        }

        private void QueueHandler_MessageRecieved(object sender, QueueEventArgs e)
        {
            SendMessageToSockets(e.Message);
        }

        public void Handle(Guid id, WebSocket webSocket)
        {
            lock (websocketConnections)
            {
                websocketConnections.Add(new SocketConnection
                {
                    Id = id,
                    WebSocket = webSocket
                });
            }

            
            while (webSocket.State == WebSocketState.Open)
            {
                //Must keep looping to keep the connection alive
            }
        }

        private  void SendMessageToSockets(Message message)
        {
            IEnumerable<SocketConnection> toSentTo;

            lock (websocketConnections)
            {
                toSentTo = websocketConnections.ToList();
            }

            var tasks = toSentTo.Select(async websocketConnection =>
            {
                var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
                var arraySegment = new ArraySegment<byte>(bytes);
                await websocketConnection.WebSocket.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
            });

            Task.WhenAll(tasks);
        }

        private void SetupCleanUpTask()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    IEnumerable<SocketConnection> openSockets;
                    IEnumerable<SocketConnection> closedSockets;

                    lock (websocketConnections)
                    {
                        openSockets = websocketConnections.Where(x => x.WebSocket.State == WebSocketState.Open || x.WebSocket.State == WebSocketState.Connecting);
                        closedSockets = websocketConnections.Where(x => x.WebSocket.State != WebSocketState.Open && x.WebSocket.State != WebSocketState.Connecting);
                        websocketConnections = openSockets.ToList();
                    }

                    foreach (var closedWebsocketConnection in closedSockets)
                    {
                        await closedWebsocketConnection.WebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "socket is closed", CancellationToken.None);
                    }

                    await Task.Delay(5000);
                }
            });
        }

    }

}
