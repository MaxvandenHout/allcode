using Microsoft.AspNetCore.Mvc;
using OpdrachtScenius.Queue;

namespace OpdrachtScenius.Controllers
{
    [ApiController]
    [Route("api")]
     public class MessageController : ControllerBase
    {
        private readonly IQueueHandler queueHandler;
        public MessageController(IQueueHandler queueHandler)
        {
            this.queueHandler = queueHandler;
        } 

        // Post api/message
        [HttpPost("message")]
        public void SendMessage(Models.Message message)
        {
            this.queueHandler.QueueMessage(message);
        }    

        // Get api/message
        //[HttpGet]
        //public ActionResult<IEnumerable<Models.Message>> GetMessagesHistory()
        //{
        //    // DB call return all
        //}

     
    }
}
