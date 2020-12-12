using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OpdrachtScenius
{
    public static class MessageExtensions
    {
        public static byte[] ToQueueBytes(this Models.Message message)
        {
            return Encoding.UTF8.GetBytes(
                JsonSerializer.Serialize(message)    
            );
        } 
    }
}
