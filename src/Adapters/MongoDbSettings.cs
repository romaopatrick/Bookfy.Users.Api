namespace Bookfy.Users.Api.src.Adapters
{
    public class MongoDbSettings
    {
        public required string Database { get; set; }
        public required string ConnectionString { get; set; }
    }
}