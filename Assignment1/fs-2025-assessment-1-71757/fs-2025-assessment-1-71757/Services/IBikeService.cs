namespace fs_2025_assessment_1_71757.Services;
using fs_2025_assessment_1_71757.Models;

    public interface IBikeService
    {
        Task<IEnumerable<StationDto>> GetStationsAsync(string? status, int? minBikes, string? query, string? sortBy, string? dir, int page, int pageSize);
        Task<StationDto?> GetStationByIdAsync(int number);
        Task<StationSummaryDto> GetSummaryAsync();
        
        List<Station> GetAllRawStations(); 
        void UpdateStationData(List<Station> updatedStations);
        
         
        Task CreateStationAsync(Station station);
        Task UpdateStationAsync(int number, Station station);
    }
