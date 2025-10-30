namespace Logging.Common.Configuration;

public class LoggingSettings
{
    public string ApplicationName { get; set; } = "MicroserviceApp";
    public string Environment { get; set; } = "Development";
    public ElasticsearchSettings? Elasticsearch { get; set; }
    public bool EnableConsoleLogging { get; set; } = true;
    public bool EnableFileLogging { get; set; } = true;
    public string MinimumLevel { get; set; } = "Information";
}

public class ElasticsearchSettings
{
    public string Uri { get; set; } = "http://localhost:9200";
    public string IndexFormat { get; set; } = "logs-{0:yyyy.MM.dd}";
}
