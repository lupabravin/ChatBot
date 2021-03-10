namespace Chat.CrossCutting.Interfaces
{
    public interface IProducer
    {
        void Produce<T>(T obj, string targetQueue, string rabbitConnection);
    }
}
