
namespace Services
{
    public interface IKafkaProducerService
    {
        void Dispose();
        Task PublishMessageAsync<T>(string topic, T message);
    }
}