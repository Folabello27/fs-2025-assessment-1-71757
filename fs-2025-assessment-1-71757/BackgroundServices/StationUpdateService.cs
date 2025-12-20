using fs_2025_assessment_1_71757.Services;

namespace fs_2025_assessment_1_71757.BackgroundServices
{
    public class StationUpdateService : BackgroundService
    {
        private readonly IBikeService _bikeService;
        private readonly Random _random = new();

        public StationUpdateService(IBikeService bikeService)
        {
            _bikeService = bikeService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                
                int delay = _random.Next(10000, 20000);
                await Task.Delay(delay, stoppingToken);

                UpdateStations();
            }
        }

        private void UpdateStations()
        {
            
            var stations = _bikeService.GetAllRawStations();

            foreach (var station in stations)
            {
                // Randomize availability
                
                int newAvailableBikes = _random.Next(0, station.BikeStands + 1);
                
                station.AvailableBikes = newAvailableBikes;
                station.AvailableBikeStands = station.BikeStands - newAvailableBikes;
                station.LastUpdate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            }

            // Commit updates
            _bikeService.UpdateStationData(stations);
            Console.WriteLine($"[Background Service] Updated {stations.Count} stations at {DateTime.Now}");
        }
    }
}