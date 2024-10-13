using EmulatorSVH.Application.Interface;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;

namespace EmulatorSVH.ReceivSend.Settings
{
    public class RabbitMQBase : IRabbitMQBase
    {
        public IModel GetConfigureRabbitMQ()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                             .Build();

            var connection = GetRabbitConnection(configuration);
            var channel = connection.CreateModel();
            return channel;
        }

        public IConnection GetRabbitConnection(IConfiguration configuration)
        {

            var rmqSettings = configuration.Get<ApplicationSettings>()?.RmqSettings;
            ConnectionFactory factory = new()
            {
                HostName = rmqSettings?.Host,
                VirtualHost = rmqSettings?.VHost,
                UserName = rmqSettings?.Login,
                Password = rmqSettings?.Password,
            };

            return factory.CreateConnection();
        }

    }
}