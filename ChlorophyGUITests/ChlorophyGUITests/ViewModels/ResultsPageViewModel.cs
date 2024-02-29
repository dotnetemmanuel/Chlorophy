using System.Text.Json;

namespace ChlorophyGUITests.ViewModels
{
    internal class ResultsPageViewModel
    {
        public List<Models.Species> SpeciesList { get; set; }

        public ResultsPageViewModel()
        {
            SpeciesList = new List<Models.Species>();

            var task = Task.Run(() => ViewModels.ResultsPageViewModel.GetSpeciesAsync(MainPage.keyword));
            task.Wait();
            SpeciesList.AddRange(task.Result);

        }
        public static async Task<List<Models.Species>> GetSpeciesAsync(string keyword)
        {
            string key = "sk-mJqa65bcd7204bed04001";
            string uri = $"/api/species-list?key={key}&q={keyword}";
            HttpClient client = new();
            client.BaseAddress = new Uri("https://perenual.com");

            List<Models.Species> speciesList = null;
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                Models.Species.SpeciesResponse speciesResponse = JsonSerializer.Deserialize<Models.Species.SpeciesResponse>(responseString);
                speciesList = speciesResponse.data;
            }

            return speciesList;
        }
    }
}
