using System.Text.Json;
using fs_2025_assessment_1_71757.Models;

namespace fs_2025_assessment_1_71757.Services
{
    public class BikeService : IBikeService
    {
        private List<Station> _stations = new();
        private readonly IWebHostEnvironment _env;

        public BikeService(IWebHostEnvironment env)
        {
            _env = env;
            LoadData(); // Load JSON on startup 
        }

        private void LoadData()
        {
            var filePath = Path.Combine(_env.ContentRootPath, "Data", "dublinbike.json");
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                _stations = JsonSerializer.Deserialize<List<Station>>(json) ?? new List<Station>();
            }
        }

        
        private StationDto MapToDto(Station s)
        {
            
            var epoch = DateTimeOffset.FromUnixTimeMilliseconds(s.LastUpdate);
            
            TimeZoneInfo dublinZone;
            try { dublinZone = TimeZoneInfo.FindSystemTimeZoneById("Greenwich Standard Time"); }
            catch { dublinZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Dublin"); }
            
            var localTime = TimeZoneInfo.ConvertTime(epoch, dublinZone).DateTime;

            
            double occupancy = s.BikeStands > 0 ? (double)s.AvailableBikes / s.BikeStands : 0;

            return new StationDto
            {
                Number = s.Number,
                Name = s.Name,
                Address = s.Address,
                Position = s.Position,
                Banking = s.Banking,
                Bonus = s.Bonus,
                BikeStands = s.BikeStands,
                AvailableBikeStands = s.AvailableBikeStands,
                AvailableBikes = s.AvailableBikes,
                Status = s.Status,
                LastUpdate = s.LastUpdate,
                LastUpdateLocal = localTime,
                Occupancy = Math.Round(occupancy * 100, 2)
            };
        }

        public async Task<IEnumerable<StationDto>> GetStationsAsync(string? status, int? minBikes, string? query, string? sortBy, string? dir, int page, int pageSize)
        {
            var queryable = _stations.AsQueryable();

            
            if (!string.IsNullOrWhiteSpace(query))
            {
                query = query.ToLower();
                queryable = queryable.Where(s => (s.Name != null && s.Name.ToLower().Contains(query)) || 
                                                 (s.Address != null && s.Address.ToLower().Contains(query)));
            }

            
            if (!string.IsNullOrWhiteSpace(status))
            {
                queryable = queryable.Where(s => s.Status != null && s.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
            }

            if (minBikes.HasValue)
            {
                queryable = queryable.Where(s => s.AvailableBikes >= minBikes.Value);
            }

            
            
            var dtos = queryable.Select(MapToDto); 

            bool isAsc = dir?.ToLower() != "desc";

            dtos = sortBy?.ToLower() switch
            {
                "name" => isAsc ? dtos.OrderBy(x => x.Name) : dtos.OrderByDescending(x => x.Name),
                "availablebikes" => isAsc ? dtos.OrderBy(x => x.AvailableBikes) : dtos.OrderByDescending(x => x.AvailableBikes),
                "occupancy" => isAsc ? dtos.OrderBy(x => x.Occupancy) : dtos.OrderByDescending(x => x.Occupancy),
                _ => dtos.OrderBy(x => x.Number) // Default sort
            };

            
            return dtos.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<StationDto?> GetStationByIdAsync(int number)
        {
            var station = _stations.FirstOrDefault(s => s.Number == number);
            return station != null ? MapToDto(station) : null;
        }

        public async Task<StationSummaryDto> GetSummaryAsync()
        {
            return new StationSummaryDto
            {
                TotalStations = _stations.Count,
                TotalBikeStands = _stations.Sum(s => s.BikeStands),
                TotalAvailableBikes = _stations.Sum(s => s.AvailableBikes),
                StatusCounts = _stations.GroupBy(s => s.Status ?? "UNKNOWN")
                                        .ToDictionary(g => g.Key, g => g.Count())
            };
        }

        // Methods used by Background Service
        public List<Station> GetAllRawStations() => _stations;
        public void UpdateStationData(List<Station> updatedStations) => _stations = updatedStations;

        public async Task CreateStationAsync(Station station) => _stations.Add(station);
        public async Task UpdateStationAsync(int number, Station station)
        {
            var index = _stations.FindIndex(s => s.Number == number);
            if (index != -1) _stations[index] = station;
        }
    }
}