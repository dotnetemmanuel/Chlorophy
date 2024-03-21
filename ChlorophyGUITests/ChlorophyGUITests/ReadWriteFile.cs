using MongoDB.Driver;

namespace ChlorophyGUITests
{
    internal class ReadWriteFile
    {
        public static void WriteAll(string fileName, string text)
        {
            string basePath = GetBasePath();

            string fullPath = Path.Combine(basePath, fileName);

            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                writer.WriteLine(text);
            }
        }

        public static async Task<Models.User> ReadAll(string fileName)
        {
            string basePath = GetBasePath();

            string fullPath = Path.Combine(basePath, fileName);

            if (File.Exists(fullPath))
            {
                using (StreamReader reader = new StreamReader(fullPath))
                {
                    string line = await reader.ReadLineAsync();
                    if (line != null)
                    {
                        Views.UserPage.SignedInUserEmail = line;
                        var currentUser = await Data.Database.ProductCollection().Find(Builders<Models.User>.Filter.Eq("Email", line)).FirstOrDefaultAsync();
                        return currentUser;
                    }
                }
            }
            return null;
        }

        public static string GetBasePath()
        {
            string appDataDirectory = FileSystem.AppDataDirectory;

            if (!appDataDirectory.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                appDataDirectory += Path.DirectorySeparatorChar;
            }

            string basePath = Path.Combine(appDataDirectory, "Files" + Path.DirectorySeparatorChar.ToString());

            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            return basePath;
        }
    }
}
