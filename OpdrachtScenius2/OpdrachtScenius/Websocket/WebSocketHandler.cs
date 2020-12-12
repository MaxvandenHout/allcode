using OpdrachtScenius.Models;
using OpdrachtScenius.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace OpdrachtScenius.Websocket
{
    public class WebsocketHandler : IWebsocketHandler
    {
        public List<SocketConnection> websocketConnections = new List<SocketConnection>();

        public WebsocketHandler(IQueueHandler queueHandler)
        {
            queueHandler.MessageRecieved += QueueHandler_MessageRecieved;
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

            //await SendMessageToSockets($"User with id <b>{id}</b> has joined the chat");

            //while (webSocket.State == WebSocketState.Open)
            //{
            //    var message = await ReceiveMessage(id, webSocket);
            //    if (message != null)
            //        await SendMessageToSockets(message);
            //}
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
                //var bytes = Encoding.UTF8.GetBytes(message);
                //var arraySegment = new ArraySegment<byte>(bytes);
                //await websocketConnection.WebSocket.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
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
                       // await SendMessageToSockets($"User with id <b>{closedWebsocketConnection.Id}</b> has left the chat");
                        await closedWebsocketConnection.WebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "socket is closed", CancellationToken.None);
                    }

                    await Task.Delay(5000);
                }
            });
        }

    }

}
