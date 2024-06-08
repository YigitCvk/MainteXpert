namespace MainteXpert.Repository.Configration
{

    public class MongoDbSettings : IMongoDbSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }

    }
}
