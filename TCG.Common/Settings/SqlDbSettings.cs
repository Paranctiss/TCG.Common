namespace TCG.Common.Settings;

public class SqlDbSettings
{
    public string Host { get; set; }
    public string Port { get; set; }
    public string DatabaseName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConnectionString => $"Server={Host};Port={Port};Database={DatabaseName};Uid={Username};Pwd={Password};";
}