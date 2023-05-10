using CommentService.EventProcessing.PostProcessors;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace CommentService.CommentBackgroundServices.PostBackgroundProcessor
{
    public class PostMessageSubscriber : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IAddPostEventProcessor _addPostEventProcessor;
        private IConnection _connection;
        private IModel _channel;
        private string _queueName;

        public PostMessageSubscriber(IConfiguration configuration, IAddPostEventProcessor addPostEventProcessor)
        {
            _configuration = configuration;
            _addPostEventProcessor = addPostEventProcessor;
            InitializeRabbitMQConfiguration();
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ModuleHandle, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _addPostEventProcessor.ProcessAddPostEvent(message);
                Console.WriteLine($"Service Received new message: {message}");
            };
            
            // Check Queuename is null or not
            // if it is not null listen to the queuename 
            if(_queueName is not null)
            {
                _channel.BasicConsume(_queueName, true, consumer);
            }
            return Task.CompletedTask;
        }

        // Or
        //private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        private void InitializeRabbitMQConfiguration()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["Host"],
                Port = int.Parse(_configuration["Port"])
            };
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare("PostExchange", ExchangeType.Direct);
                _queueName = "Post";
                _channel.QueueBind(_queueName, "PostExchange","PostKey");
                _connection.ConnectionShutdown += _connection_ConnectionShutdown;
                Console.WriteLine("Connection has been created");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not connect to the rabbitmq: {ex.Message}");
            }
        }

        private void _connection_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("Connection shutdown");
        }

        // Dispose Method
        public override void Dispose()
        {
            if(_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
            base.Dispose();
        }
    }
}
