using Application.Extensions;
using Application.Interfaces;
using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Kafka;

public class KafkaExternalEventConsumerService<T> : BackgroundService
    where T: class, IExternalEvent
{
    // private readonly ConsumerConfig _consumerConfig;
    private readonly IMediator _mediator;
    private readonly ILogger<KafkaExternalEventConsumerService<T>> _logger;
    private readonly IConfiguration _configuration;

    public KafkaExternalEventConsumerService(
        // ConsumerConfig consumerConfig, 
        IMediator mediator, 
        ILogger<KafkaExternalEventConsumerService<T>> logger, 
        IConfiguration configuration)
    {
        // _consumerConfig = consumerConfig;
        _mediator = mediator;
        _logger = logger;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Run(() => StartConsumer(stoppingToken), stoppingToken);
    }

    private void StartConsumer(CancellationToken stoppingToken)
    {
        var consumerConfig = KafkaExtensions.CreateConsumerConfig(
            groupId: Guid.NewGuid().ToString(),
            _configuration.GetSettingsKafkaBootstrapServer());
        
        using var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
        consumer.Subscribe( _configuration.GetSettingsKafkaTopic());
        
        _logger.LogInformation("Start {Name}:{TypeName}", GetType().Name, typeof(T).Name);
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var msg = consumer.Consume(stoppingToken);
                var msgNotification = JsonConvert.DeserializeObject(msg.Message.Value, typeof(T));
                if (msgNotification is not null)
                    _mediator.Publish(msgNotification, stoppingToken).GetAwaiter().GetResult();
            }
            catch (OperationCanceledException) { }
        }
    }
}