using br.users.application.test.domain.Entities.Messasing;
using br.users.application.test.domain.Entities.UserCx;
using br.users.application.test.domain.Interfaces.Messaging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;


namespace br.users.application.test.messasing
{
    public class MessageBusService : IMessageBusService
    {
        private readonly IConfiguration _configuration;
        public MessageBusService(IConfiguration configuration)
        {
           _configuration = configuration;
        }

        public void PublishMessage(UserDTO dto)
        {

            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _configuration["RabbitMQ:HostName"] ?? "localhost",
                    Port = 5672,
                    UserName = "guest",
                    Password = "guest"
                };

                using var connection = factory.CreateConnection();
                Console.WriteLine("✅ Conectado com sucesso!");
                using var channel = connection.CreateModel();

                channel.QueueDeclare(queue: "user_created", durable: false, exclusive: false, autoDelete: false, arguments: null);

                var message = System.Text.Json.JsonSerializer.Serialize(dto);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: "user_created", body: body);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"[PublishMessage] - Erro ao publicar uma mensagem na fila de criação de usuários: {ex.Message}");

            }
            
        }
    }
}
