using PostService.Models;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace PostService.ServiceBus
{
    public class PostServiceBus : IPostServiceBus
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public PostServiceBus(IConfiguration configuration)
        {
            _configuration = configuration;
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
                _channel.QueueDeclare("Post", false, false, false, null);
                _channel.QueueBind("Post", "PostExchange", "PostKey", null);
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
            Console.WriteLine("Connection has been shutdown");
        }

        public void PublishNewPost(Post post)
        {
            var message = JsonSerializer.Serialize(post);
            if(_connection is not null)
            {
                if(_connection.IsOpen)
                {
                    Console.WriteLine("RabbitMQ Connection Is Open");
                    var result = SendMessage(message);
                    if(result is true)
                    {
                        Console.WriteLine($"{message} has been sent");
                    }
                }
            }
        }

        private bool SendMessage(string message)
        {
            try
            {
                var body = Encoding.UTF8.GetBytes(message);
                _channel.BasicPublish("PostExchange", "PostKey", null, body);
                
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
