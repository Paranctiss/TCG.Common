namespace TCG.Common.Settings;

public class MongoDbSettings
{
    public string Host { get; set; }
    public string Port { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
    public string ConnectionString => $"mongodb://{User}:{Password}@{Host}:{Port}/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@cscmongodb@";

}