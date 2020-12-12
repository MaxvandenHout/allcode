using OpdrachtScenius.Models;
using System;

namespace OpdrachtScenius.Queue
{
    public interface IQueueHandler
    {
        void QueueMessage(Message message);

        event EventHandler<QueueEventArgs> MessageRecieved;
    }
}
