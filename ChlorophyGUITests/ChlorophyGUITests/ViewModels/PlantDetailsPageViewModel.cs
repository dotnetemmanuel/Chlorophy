using MongoDB.Driver;
using System.Text.Json;

namespace ChlorophyGUITests.ViewModels
{
    class PlantDetailsPageViewModel
    {
        public Models.PlantDetails Plant { get; set; }

        public static int SpeciesId { get; set; }

        public PlantDetailsPageViewModel()
        {

        }

        public async Task InitializeAPIAsync()
        {
            Plant = await GetPlantDetailsFromAPI(SpeciesId);
        }

        public async Task InitializeDbAsync()
        {
            Plant = await GetPlantDetailsFromDb(SpeciesId);
        }


        public void SetSpeciesId(int speciesId)
        {
            SpeciesId = speciesId;
        }

        public static async Task<Models.PlantDetails> GetPlantDetailsFromAPI(int id)
        {
            string key = "sk-mJqa65bcd7204bed04001";
            string uri = $"/api/species/details/{id}?key={key}";
            HttpClient client = new();
            client.BaseAddress = new Uri("https://perenual.com");

            Models.PlantDetails plant = null;
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();

                plant = JsonSerializer.Deserialize<Models.PlantDetails>(responseString);
                plant.WateringDate = null;
                plant.WateringMessage = null;
                string frequency = plant.watering_general_benchmark.value;
                if (frequency != null)
                {
                    if (frequency.Length > 1)
                    {
                        string[] parts = frequency.Split("-");
                        string firstPart = parts[0];
                        plant.WateringFrequency = int.Parse(firstPart);
                    }
                    else
                    {
                        plant.WateringFrequency = int.Parse(frequency);
                    }
                }
                else
                {
                    plant.WateringFrequency = null;
                }

                return plant;
            }
            else
            {
                return null;
            }
        }

        public static async Task<Models.PlantDetails> GetPlantDetailsFromDb(int id)
        {
            var currentUser = Data.Database.ProductCollection().Find(Builders<Models.User>.Filter.Eq("Email", Views.UserPage.SignedInUserEmail)).FirstOrDefault();
            var plant = currentUser.Plants.Find(p => p.id == id);

            return plant;
        }


    }
}
