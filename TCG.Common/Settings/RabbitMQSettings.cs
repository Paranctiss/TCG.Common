namespace TCG.Common.Settings;

public class RabbitMQSettings
{
    public string Host { get; init; }
    public string QueueName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}