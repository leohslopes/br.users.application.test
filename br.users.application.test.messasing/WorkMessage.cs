using br.users.application.test.domain.Entities.Messasing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mysqlx.Crud;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace br.users.application.test.messasing
{
    public class WorkMessage : BackgroundService
    {
        private readonly ILogger<WorkMessage> _logger;
        private IConnection _connection;
        private IModel _channel;

        public WorkMessage(ILogger<WorkMessage> logger)
        {
            _logger = logger;

            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "user_created", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var user = JsonSerializer.Deserialize<UserDTO>(message);
                _logger.LogInformation($"👤 Usuário recebido: {user?.UserName} - {user?.UserEmail} - {user?.DatePublisher}");

                // Aqui você pode salvar no banco, enviar e-mail, etc.
            };

            _channel.BasicConsume(queue: "user_created", autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }
    }
}
