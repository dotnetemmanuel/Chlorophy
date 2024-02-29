using MongoDB.Driver;

namespace ChlorophyGUITests.ViewModels
{
    public class MainPageViewModel
    {
        public Models.User User { get; set; }
        public List<Models.PlantDetails> PlantDetails { get; set; }
        public string WelcomeMessage { get; set; }
        public string Suggestion { get; set; }

        public MainPageViewModel()
        {
            PlantDetails = new();
            User = new();

            try
            {
                var task = Task.Run(() => GetCurrentUser());
                task.Wait();
                var currentUser = task.Result;

                if (currentUser != null)
                {
                    User = currentUser;
                    PlantDetails.AddRange(currentUser.Plants);
                    WelcomeMessage = $"Welcome, {currentUser.Firstname}";
                    Suggestion = "Get started by searching for a plant and adding it to your list";
                }
                else
                {
                    WelcomeMessage = "Welcome!";
                    Suggestion = "Get started by singning into your account or by creating one!";
                }
            }
            catch (Exception)
            {
                WelcomeMessage = "Error!";
                Suggestion = "An error occurred while retrieving user data.";
            }
        }

        private async Task<Models.User> GetCurrentUser()
        {
            if (Views.UserPage.SignedInUserEmail != null)
            {
                string currentUserEmail = Views.UserPage.SignedInUserEmail;
                var currentUser = await Data.Database.ProductCollection().Find(Builders<Models.User>.Filter.Eq("Email", currentUserEmail)).FirstOrDefaultAsync();

                return currentUser;
            }
            else
            {
                return null;
            }
        }
    }
}
