using Microsoft.AspNetCore.Mvc;
using OpdrachtScenius.Websocket;
using System;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace OpdrachtScenius.Controllers
{
    [Route("api/[controller]")]
    public class WebSocketController : Controller
    {
        public IWebsocketHandler WebsocketHandler { get; }

        public WebSocketController(IWebsocketHandler websocketHandler)
        {
            WebsocketHandler = websocketHandler;
        }

        [HttpGet]
        public async Task Get()
        {
            var context = ControllerContext.HttpContext;
            var isSocketRequest = context.WebSockets.IsWebSocketRequest;

            if (isSocketRequest)
            {
                WebSocket websocket = await context.WebSockets.AcceptWebSocketAsync();
                WebsocketHandler.Handle(Guid.NewGuid(), websocket);
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        }
    }
}