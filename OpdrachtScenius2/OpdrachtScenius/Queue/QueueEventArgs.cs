using OpdrachtScenius.Models;
using System;

namespace OpdrachtScenius.Queue
{
    public class QueueEventArgs : EventArgs
    {
        public Message Message { get; set; }
    }
}
