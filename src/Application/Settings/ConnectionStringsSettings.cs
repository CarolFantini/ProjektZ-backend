namespace Application.Settings
{
    public class ConnectionStringsSettings
    {
        public string Provider { get; set; } = "sqlserver"; // sqlserver, postgresql or mongodb
        public string Database { get; set; } = string.Empty;
        public string MongoDbName { get; set; } = string.Empty; // only for MongoDB
    }

}
