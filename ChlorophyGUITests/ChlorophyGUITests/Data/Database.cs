using MongoDB.Driver;
using System.Security.Authentication;


namespace ChlorophyGUITests.Data
{
    class Database
    {
        private static MongoClient GetClient()
        {
            string connectionString;
            MongoClient mongoClient;

            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                const string connectionUri = "mongodb://admin_emm_duc_sys_23_1:NDWsBTWVOyLDkxsY@ac-kqbt0az-shard-00-00.mcit2k6.mongodb.net:27017,ac-kqbt0az-shard-00-01.mcit2k6.mongodb.net:27017,ac-kqbt0az-shard-00-02.mcit2k6.mongodb.net:27017/?ssl=true&replicaSet=atlas-8l5b4f-shard-0&authSource=admin&retryWrites=true&w=majority&appName=Cluster0";

                var settings = MongoClientSettings.FromConnectionString(connectionUri);

                // Set the ServerApi field of the settings object to set the version of the Stable API on the client
                settings.ServerApi = new ServerApi(ServerApiVersion.V1);

                // Create a new client and connect to the server
                mongoClient = new MongoClient(settings);

            }
            else
            {
                connectionString = "mongodb+srv://admin_emm_duc_sys_23_1:NDWsBTWVOyLDkxsY@cluster0.mcit2k6.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
                settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                mongoClient = new MongoClient(settings);

            }

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
