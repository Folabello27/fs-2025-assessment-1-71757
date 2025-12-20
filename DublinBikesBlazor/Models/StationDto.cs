using System.Text.Json.Serialization;
using fs_2025_assessment_1_71757.Models;

namespace DublinBikesBlazor.Models;

public class StationDto
{
    public int Number { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int BikeStands { get; set; }
    public int AvailableBikes { get; set; }
    public int AvailableBikeStands { get; set; }
    public string Status { get; set; } = string.Empty;
    public double Occupancy { get; set; }
    public DateTime LastUpdateLocal { get; set; }
    
    // Links the UI fields to the API's nested Position object
    public StationPosition? Position { get; set; }
    public double Latitude => Position?.Lat ?? 0;
    public double Longitude => Position?.Lng ?? 0;
}