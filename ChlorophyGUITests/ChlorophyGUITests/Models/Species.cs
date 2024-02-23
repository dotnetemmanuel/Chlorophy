namespace ChlorophyGUITests.Models
{
    class Species
    {
        public class SpeciesResponse
        {
            public List<Models.Species> data { get; set; }
        }

        public int id { get; set; }
        public string? common_name { get; set; }
        public List<string>? scientific_name { get; set; }
        public string? cycle { get; set; }
        public string? watering { get; set; }
        //public List<string>? sunlight { get; set; }
        public List<string>? other_name { get; set; }
        public Default_Image? default_image { get; set; }
    }

    public class Default_Image
    {
        public int? image_id { get; set; }
        public int? license { get; set; }
        public string? license_name { get; set; }
        public string? license_url { get; set; }
        public string? original_url { get; set; }
        public string? regular_url { get; set; }
        public string? medium_url { get; set; }
        public string? small_url { get; set; }
        public string? thumbnail { get; set; }
    }
}
