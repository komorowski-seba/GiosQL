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
    private readonly IMediator _mediator;
    private readonly ILogger<KafkaExternalEventConsumerService<T>> _logger;
    private readonly IConfiguration _configuration;

    public KafkaExternalEventConsumerService(
        IMediator mediator, 
        ILogger<KafkaExternalEventConsumerService<T>> logger, 
        IConfiguration configuration)
    {
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
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = _configuration.GetSettingsKafkaBootstrapServer(),
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = true,
            GroupId = Guid.NewGuid().ToString()
        };
        
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