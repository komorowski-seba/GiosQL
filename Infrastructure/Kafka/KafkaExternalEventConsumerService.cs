using System.Reflection;
using System.Text;
using Application.Interfaces;
using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Infrastructure.Kafka;

public class KafkaExternalEventConsumerService<T> : BackgroundService
    where T: class, IExternalEvent
{
    private readonly ConsumerConfig _consumerConfig;
    private readonly IMediator _mediator;

    public KafkaExternalEventConsumerService(ConsumerConfig consumerConfig, IMediator mediator)
    {
        _consumerConfig = consumerConfig;
        _mediator = mediator;
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