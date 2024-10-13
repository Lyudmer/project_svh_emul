﻿using EmulatorSVH.Application.Interface;
using RabbitMQ.Client;

using System.Text;


namespace EmulatorSVH.ReceivSend.Producer
{
    public class RabbitMQProducer(IRabbitMQBase rabbitMQBase) : IMessagePublisher
    {
        private readonly IRabbitMQBase _rabbitMQBase = rabbitMQBase;
        public void SendMessage<T>(T xPkg, string CodeCMN)
        {
            using IModel channel = _rabbitMQBase.GetConfigureRabbitMQ();

            channel.QueueDeclare(CodeCMN, exclusive: false);
            var strPkg = xPkg?.ToString();
            if (strPkg != null)
            {
                var body = Encoding.UTF8.GetBytes(strPkg);

                channel.BasicPublish(exchange: "package", routingKey: CodeCMN, body: body);
            }
        }
    }
}
