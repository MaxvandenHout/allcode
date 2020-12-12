using OpdrachtScenius.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

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
            // It is inefficient to create new connection and channel for every api request.
            // TODO: Channel and connection need to be properly closed after X seconds idle.
            if (queueConnection == null)
            {
                queueConnection = (new ConnectionFactory() { HostName = "localhost" }).CreateConnection();
                queueChannel = queueConnection.CreateModel();
                queueChannel.QueueDeclare(queue: QueueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
            }
            return queueChannel;
        }

        public void QueueMessage(Models.Message message)
        {
            GetQueueChannel().BasicPublish(null, QueueName, null, message.ToQueueBytes());
        }

        public void RecieveMessages()
        {
            var channel = GetQueueChannel();
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                // copy or deserialise the payload
                // and process the message
                // ...
                channel.BasicAck(ea.DeliveryTag, false);
            };
            // this consumer tag identifies the subscription
            // when it has to be cancelled
            String consumerTag = channel.BasicConsume(QueueName, false, consumer);

        }
    }
}
