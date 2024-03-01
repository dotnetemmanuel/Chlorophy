using MongoDB.Driver;

namespace ChlorophyGUITests.ViewModels
{


    public class UserPageViewModel
    {
        public Models.User User { get; set; }

        public UserPageViewModel()
        {
            User = new();

            var task = Task.Run(() => GetCurrentUser());
            task.Wait();
            var currentUser = task.Result;

            if (currentUser != null)
            {
                User = currentUser;
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
