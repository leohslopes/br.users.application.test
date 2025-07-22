using br.users.application.test.domain.Interfaces.Messaging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json.Nodes;


namespace br.users.application.test.messasing
{
    public class MessageBusService : IMessageBusService
    {

        private readonly IConnection _connection;
        private readonly IModel _model;
        private const string _exchange = "trackings-service";

        public MessageBusService()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            //_connection = connectionFactory.CreateConnection("trackings-service-publisher");
            //_model = _connection.CreateModel();

        }

        public void PublishMessage(object data, string routingKey)
        {
            var type = data.GetType();
            var payLoad = JsonConvert.SerializeObject(data);
            var byteArray = Encoding.UTF8.GetBytes(payLoad);

            Console.WriteLine($"{type.Name} Published.");
            _model.BasicPublish(_exchange,routingKey,null, byteArray);
        }
    }
}
