using ChlorophyGUITests.Models;
using MongoDB.Driver;

namespace ChlorophyGUITests.ViewModels
{
    public class MainPageViewModel
    {
        public Models.User User { get; set; }
        public List<Models.PlantDetails> PlantDetails { get; set; }
        public string WelcomeMessage { get; set; }
        public string Suggestion { get; set; }
        public string WateringMessage { get; set; }

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
                    foreach (var plant in currentUser.Plants)
                    {
                        var task2 = Task.Run(() => WateringReminder(plant));
                        task2.Wait();
                        plant.WateringMessage = task2.Result;
                    }
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

        public static async Task<Models.User> GetCurrentUser()
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

        public async Task<string> WateringReminder(PlantDetails plant)
        {
            string wateringReminder = "";

            //var currentUser = await GetCurrentUser();
            if (plant.WateringFrequency != null)
            {
                if (plant.WateringDate != null)
                {
                    if (plant.WateringDate != null)
                    {
                        TimeSpan difference = MainPage.Today.Subtract((DateTime)plant.WateringDate);
                        int dateDifference = difference.Days;

                        if (dateDifference != plant.WateringFrequency)
                        {
                            wateringReminder = (plant.WateringFrequency - (dateDifference)).ToString() + " days before watering";

                        }
                        else
                        {
                            wateringReminder = "I need water!";

                        }
                    }
                }
            }
            else
            {
                wateringReminder = "No watering information";
            }

            return wateringReminder;
        }
    }
}
