namespace Messaging.Kafka.Configuration;

/// <summary>
/// Configuration settings for Apache Kafka connection and behavior.
/// These settings follow the Options pattern for .NET configuration.
/// </summary>
public class KafkaSettings
{
    /// <summary>
    /// Gets or sets the Kafka broker addresses (comma-separated list of host:port)
    /// </summary>
    public string BootstrapServers { get; set; } = "localhost:9092";
    
    /// <summary>
    /// Gets or sets the consumer group ID for coordinating message consumption
    /// </summary>
    public string GroupId { get; set; } = "default-group";
    
    /// <summary>
    /// Gets or sets whether to automatically commit message offsets
    /// </summary>
    public bool EnableAutoCommit { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the interval (in milliseconds) for automatic offset commits
    /// </summary>
    public int AutoCommitIntervalMs { get; set; } = 5000;
    
    /// <summary>
    /// Gets or sets where to start reading when no offset is available ("earliest" or "latest")
    /// </summary>
    public string AutoOffsetReset { get; set; } = "earliest";
}
