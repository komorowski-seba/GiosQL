using System.Text;
using Application.Interfaces;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Kafka;

public class KafkaExternalEventPushService<T> : IExternalEventService<T>, IDisposable
    where T: class, IExternalEvent
{
     private readonly Lazy<IProducer<string, string>> _producer;
     private readonly ILogger<KafkaExternalEventPushService<T>> _logger;

     public KafkaExternalEventPushService(ConsumerConfig consumerConfig, ILogger<KafkaExternalEventPushService<T>> logger)
     {
         _logger = logger;
         _producer = new Lazy<IProducer<string, string>>(() => new ProducerBuilder<string, string>(consumerConfig).Build());
     }

     private async Task SendAsync(T msg)
     {
         const string key = "KafkaProducerService";
         var serialisedMessage = JsonConvert.SerializeObject(msg);
         var messageType = msg.GetType().Name;
         try
         {
             var producedMessage = new Message<string, string>
             {
                 Key = key,
                 Value = serialisedMessage,
                 Headers = new Headers {{"message-type", Encoding.UTF8.GetBytes(messageType)}}
             };
             await _producer.Value.ProduceAsync("simpletalk_topic", producedMessage);
             
             Console.WriteLine($" ##### >>> '{serialisedMessage}'");
         }
         catch (Exception e)
         {
             _logger.LogWarning(
                 "I can't send a Message: '{Message}'; Type: '{Type}'; Key: '{Key}'",
                 serialisedMessage,
                 messageType,
                 key); 
         }
     }
     
     public async Task Publish(T evt)
     {
         await SendAsync(evt);
     }

    public void Dispose()
    {
        if (_producer.IsValueCreated)
            _producer.Value.Dispose();
    }
}