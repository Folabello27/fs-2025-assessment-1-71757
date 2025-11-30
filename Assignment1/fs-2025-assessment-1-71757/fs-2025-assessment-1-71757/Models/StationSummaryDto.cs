namespace fs_2025_assessment_1_71757.Models;

public class StationSummaryDto
{
    public int TotalStations { get; set; }
    public int TotalBikeStands { get; set; }
    public int TotalAvailableBikes { get; set; }
    public Dictionary<string, int> StatusCounts { get; set; } = new();
}