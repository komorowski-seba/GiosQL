using System.Reflection;
using System.Text;
using Application.Interfaces;
using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Kafka;

public class KafkaExternalEventConsumerService<T> : BackgroundService
    where T: class, IExternalEvent
{
    private readonly ConsumerConfig _consumerConfig;
    private readonly IMediator _mediator;
    private readonly ILogger<KafkaExternalEventConsumerService<T>> _logger;

    public KafkaExternalEventConsumerService(
        ConsumerConfig consumerConfig, 
        IMediator mediator, 
        ILogger<KafkaExternalEventConsumerService<T>> logger)
    {
        _consumerConfig = consumerConfig;
        _mediator = mediator;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Run(() => StartConsumer(stoppingToken), stoppingToken);
    }

    private void StartConsumer(CancellationToken stoppingToken)
    {
        using var consumer = new ConsumerBuilder<string, string>(_consumerConfig).Build();
        consumer.Subscribe("simpletalk_topic");

        var findTypes = Assembly
            .Load(nameof(Application))
            .GetTypes()
            .Where(n => n.GetInterfaces().Any(m => m == typeof(T)))
            .ToList();
        
        _logger.LogInformation(" ----- >>>> kafka start");
        
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var msg = consumer.Consume(stoppingToken);
                // todo move to config 
                var msgType = Encoding.UTF8.GetString(msg.Message.Headers.GetLastBytes("message-type"));
                var findMsgType = findTypes.FirstOrDefault(n => n.Name.Equals(msgType));
                if (findMsgType is null)
                    continue;

                var msgNotification = JsonConvert.DeserializeObject(msg.Message.Value, findMsgType);
                if (msgNotification is not null)
                    _mediator.Publish(msgNotification, stoppingToken).GetAwaiter().GetResult();
            }
            catch (OperationCanceledException) { }
        }
    }
}