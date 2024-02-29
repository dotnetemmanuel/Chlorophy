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

        public async Task InitializeAsync()
        {
            Plant = await GetPlantDetails(SpeciesId);
        }


        public void SetSpeciesId(int speciesId)
        {
            SpeciesId = speciesId;

        }

        public static async Task<Models.PlantDetails> GetPlantDetails(int id)
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

                return plant;
            }
            else
            {
                return null;
            }
        }
    }
}
