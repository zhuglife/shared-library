using Confluent.Kafka;
using Messaging.Events.Base;
using Messaging.Kafka.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Messaging.Kafka.Services;

/// <summary>
/// Service for publishing integration events to Apache Kafka topics.
/// Implements the Producer pattern for message-based communication.
/// </summary>
public class KafkaProducer : IDisposable
{
    private readonly IProducer<string, string> _producer;
    private readonly ILogger<KafkaProducer> _logger;

    /// <summary>
    /// Initializes a new instance of KafkaProducer with dependency injection
    /// </summary>
    /// <param name="settings">Kafka configuration settings wrapped in IOptions</param>
    /// <param name="logger">Logger for diagnostic information</param>
    public KafkaProducer(IOptions<KafkaSettings> settings, ILogger<KafkaProducer> logger)
    {
        _logger = logger;
        
        var config = new ProducerConfig
        {
            BootstrapServers = settings.Value.BootstrapServers
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    /// <summary>
    /// Publishes an integration event to the specified Kafka topic
    /// </summary>
    /// <typeparam name="T">The type of integration event to publish</typeparam>
    /// <param name="topic">The Kafka topic name where the message will be published</param>
    /// <param name="message">The integration event to publish</param>
    /// <returns>A task representing the asynchronous operation</returns>
    /// <exception cref="Exception">Thrown when publishing fails</exception>
    public async Task PublishAsync<T>(string topic, T message) where T : IntegrationEvent
    {
        try
        {
            var json = JsonSerializer.Serialize(message);
            var kafkaMessage = new Message<string, string>
            {
                Key = message.Id.ToString(),
                Value = json
            };

            var result = await _producer.ProduceAsync(topic, kafkaMessage);
            _logger.LogInformation(
                "Message published to topic '{Topic}' with key '{Key}' at offset {Offset}", 
                topic, result.Key, result.Offset);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error publishing message to topic '{Topic}'", topic);
            throw;
        }
    }

    /// <summary>
    /// Releases the resources used by the Kafka producer
    /// </summary>
    public void Dispose()
    {
        _producer?.Dispose();
    }
}
