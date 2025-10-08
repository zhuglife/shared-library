namespace Messaging.Kafka.Configuration;

public class KafkaConfiguration
{
    public string BootstrapServers { get; set; } = "localhost:9092";
    public string GroupId { get; set; } = "default-group";
    public bool EnableAutoCommit { get; set; } = true;
    public int AutoCommitIntervalMs { get; set; } = 5000;
    public string AutoOffsetReset { get; set; } = "earliest";
}
