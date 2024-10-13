namespace EmulatorSVH.ReceivSend.Producer
{
    public interface IMessagePublisher
    {
        void SendMessage<T>(T message, string CodeCMN);
    }
}
