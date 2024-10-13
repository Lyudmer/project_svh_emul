using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace EmulatorSVH.Application.Interface
{
    public interface IRabbitMQBase
    {
        IModel GetConfigureRabbitMQ();
        IConnection GetRabbitConnection(IConfiguration configuration);
    }
}