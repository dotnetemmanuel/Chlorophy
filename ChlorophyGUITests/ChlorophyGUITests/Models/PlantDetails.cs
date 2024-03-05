namespace ChlorophyGUITests.Models
{
    public class PlantDetails
    {
        public int? id { get; set; }
        public string? common_name { get; set; }
        public List<string>? scientific_name { get; set; }
        public List<string>? other_name { get; set; }
        public string? family { get; set; }
        public List<string>? origin { get; set; }
        public string? type { get; set; }
        public string? dimension { get; set; }
        public Dimensions? dimensions { get; set; }
        public string? cycle { get; set; }
        public List<string>? attracts { get; set; }
        public List<string>? propagation { get; set; }
        public Hardiness? hardiness { get; set; }
        public HardinessLocation? hardiness_location { get; set; }
        public string? watering { get; set; }
        //public List<int>? depth_water_requirement { get; set; }
        //public WaterVolume? volume_water_requirement { get; set; }
        public string? watering_period { get; set; }
        public Benchmark? watering_general_benchmark { get; set; }
        public List<PlantAnatomy>? plant_anatomy { get; set; }
        public List<string>? sunlight { get; set; }
        public List<string>? pruning_month { get; set; }
        //public PruningCount? pruning_count { get; set; }
        public int? seeds { get; set; }
        public string? maintenance { get; set; }
        public string? care_guides { get; set; }
        public List<string>? soil { get; set; }
        public string? growth_rate { get; set; }
        public bool? drought_tolerant { get; set; }
        public bool? salt_tolerant { get; set; }
        public bool? thorny { get; set; }
        public bool? invasive { get; set; }
        public bool? tropical { get; set; }
        public bool? indoor { get; set; }
        public string? care_level { get; set; }
        public List<string>? pest_susceptibility { get; set; }
        public string? pest_susceptibility_api { get; set; }
        public bool? flowers { get; set; }
        public string? flowering_season { get; set; }
        public string? flower_color { get; set; }
        public bool? cones { get; set; }
        public bool? fruits { get; set; }
        public bool? edible_fruit { get; set; }
        public string? edible_fruit_taste_profile { get; set; }
        public string? fruit_nutritional_value { get; set; }
        public List<string>? fruit_color { get; set; }
        public string? harvest_season { get; set; }
        public bool? leaf { get; set; }
        public List<string>? leaf_color { get; set; }
        public bool? edible_leaf { get; set; }
        public bool? cuisine { get; set; }
        public bool? medicinal { get; set; }
        public int? poisonous_to_humans { get; set; }
        public int? poisonous_to_pets { get; set; }
        public string? description { get; set; }
        public DefaultImage? default_image { get; set; }
        public string? other_images { get; set; }

        public DateTime? WateringDate { get; set; } = null;
        public int? WateringFrequency { get; set; } = null;
        public string? WateringMessage { get; set; } = null;
    }

    public class WaterVolume
    {
        public int? value { get; set; }
        public string? unit { get; set; }
    }

    public class Dimensions
    {
        public string? type { get; set; }
        public double? min_value { get; set; }
        public double? max_value { get; set; }
        public string? unit { get; set; }
    }

    public class Hardiness
    {
        public string? min { get; set; }
        public string? max { get; set; }
    }

    public class HardinessLocation
    {
        public string? full_url { get; set; }
        public string? full_iframe { get; set; }
    }

    public class Benchmark
    {
        public string? value { get; set; }
        public string? unit { get; set; }
    }

    public class PlantAnatomy
    {
        public string? part { get; set; }
        public List<string>? color { get; set; }
    }

    public class PruningCount
    {
        public string? amount { get; set; }
        public string? interval { get; set; }
    }

    public class DefaultImage
    {
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
