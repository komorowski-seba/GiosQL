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

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var msg = consumer.Consume(stoppingToken);
                if (JsonConvert.DeserializeObject(msg.Message.Value, typeof(T)) is T msgNotification)
                    _mediator.Publish(msgNotification, stoppingToken).GetAwaiter().GetResult();
            }
            catch (OperationCanceledException) { }
        }
    }
}