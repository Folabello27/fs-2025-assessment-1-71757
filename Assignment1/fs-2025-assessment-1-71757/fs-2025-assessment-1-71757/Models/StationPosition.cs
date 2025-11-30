using System.Text.Json.Serialization;

namespace fs_2025_assessment_1_71757.Models;

public class StationPosition
{
    [JsonPropertyName("lat")]
    public double Lat { get; set; }

    [JsonPropertyName("lng")]
    public double Lng { get; set; }
}