using OpdrachtScenius.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;

namespace OpdrachtScenius.Queue
{
    public class QueueHandler : IQueueHandler
    {
        private const string QueueName = "messages";

        private IConnection queueConnection = null;
        private IModel queueChannel = null;

        public event EventHandler<QueueEventArgs> MessageRecieved;

        private void OnMessageRecieved(Message message)
        {
            if (MessageRecieved != null)
            {
                MessageRecieved(this, new QueueEventArgs { Message = message });
            }
        }

        private IModel GetQueueChannel()
        {
            if (queueConnection == null)
            {
                queueConnection = (new ConnectionFactory() { HostName = "localhost" }).CreateConnection();
                queueChannel = queueConnection.CreateModel();
                queueChannel.QueueDeclare(QueueName, false, false, false, null);
                RecieveMessages(queueChannel);
                    
            }
            return queueChannel;
        }

        public void QueueMessage(Message message)
        {
            GetQueueChannel().BasicPublish(
                "",
                QueueName,
                null,
                Encoding.UTF8.GetBytes(
                    JsonSerializer.Serialize(message)
                )
            );
        }


        private void RecieveMessages(IModel channel)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {
                OnMessageRecieved(
                    JsonSerializer.Deserialize<Message>(
                        Encoding.UTF8.GetString(
                             ea.Body.ToArray()
                        )
                    )
                );
                channel.BasicAck(ea.DeliveryTag, false);
            };
            // this consumer tag identifies the subscription
            // when it has to be cancelled
            String consumerTag = channel.BasicConsume(QueueName, false, consumer);

        }
    }
}
