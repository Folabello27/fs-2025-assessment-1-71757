using System.Text.Json.Serialization;

namespace fs_2025_assessment_1_71757.Models
{
    public class Station
    {
        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("contract_name")]
        public string? ContractName { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("position")]
        public StationPosition? Position { get; set; }

        [JsonPropertyName("banking")]
        public bool Banking { get; set; }

        [JsonPropertyName("bonus")]
        public bool Bonus { get; set; }

        [JsonPropertyName("bike_stands")]
        public int BikeStands { get; set; }

        [JsonPropertyName("available_bike_stands")]
        public int AvailableBikeStands { get; set; }

        [JsonPropertyName("available_bikes")]
        public int AvailableBikes { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("last_update")]
        public long LastUpdate { get; set; } // Epoch ms 
    }
}