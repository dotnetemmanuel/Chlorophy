using MongoDB.Driver;
using System.Security.Authentication;

namespace ChlorophyGUITests.Data
{
    class Database
    {
        private static MongoClient GetClient()
        {
            string connectionString = "mongodb+srv://admin_emm_duc_sys_23_1:NDWsBTWVOyLDkxsY@cluster0.mcit2k6.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";

            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);

            return mongoClient;
        }

        public static IMongoCollection<Models.User> ProductCollection()
        {
            var client = GetClient();

            var database = client.GetDatabase("Chlorophy");

            var productCollection = database.GetCollection<Models.User>("User");

            return productCollection;
        }
    }
}
