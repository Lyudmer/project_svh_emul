namespace EmulatorSVH.ReceivSend.Consumer
{
    public interface IRabbitMQConsumer
    {
        string LoadMessage(string CodeCMN);
    }
}