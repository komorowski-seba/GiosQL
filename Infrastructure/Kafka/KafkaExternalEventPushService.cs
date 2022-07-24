using Application.Extensions;
using Application.Interfaces;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Kafka;

public class KafkaExternalEventPushService<T> : IExternalEventService<T>, IDisposable
    where T: class, IExternalEvent
{
     private readonly Lazy<IProducer<string, string>> _producer;
     private readonly ILogger<KafkaExternalEventPushService<T>> _logger;
     private readonly IConfiguration _configuration;

     public KafkaExternalEventPushService(ILogger<KafkaExternalEventPushService<T>> logger, IConfiguration configuration)
     {
         _logger = logger;
         _configuration = configuration;
         _producer = new Lazy<IProducer<string, string>>(
             () => new ProducerBuilder<string, string>(new ConsumerConfig
             {
                 BootstrapServers = _configuration.GetSettingsKafkaBootstrapServer(),
                 AutoOffsetReset = AutoOffsetReset.Earliest,
                 EnableAutoCommit = true,
             })
            .Build());
     }

     private async Task SendAsync(T msg)
     {
         var serialisedMessage = JsonConvert.SerializeObject(msg);
         var messageType = msg.GetType().Name;
         try
         {
             var producedMessage = new Message<string, string>
             {
                 Key = _configuration.GetSettingsKafkaKey(),
                 Value = serialisedMessage,
             };
             await _producer.Value.ProduceAsync(_configuration.GetSettingsKafkaTopic(), producedMessage);
         }
         catch (Exception e)
         {
             _logger.LogWarning(
                 "I can't send a Message: '{Message}'; Type: '{Type}'; Key: '{Key}'; Error: '{Ex}'",
                 serialisedMessage,
                 messageType,
                 _configuration.GetSettingsKafkaKey(),
                 e.Message); 
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