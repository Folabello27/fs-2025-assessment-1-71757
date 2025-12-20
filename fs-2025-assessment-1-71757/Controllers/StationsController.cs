using Asp.Versioning;
using fs_2025_assessment_1_71757.Models;
using fs_2025_assessment_1_71757.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace fs_2025_assessment_1_71757.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/stations")]
    public class StationsController : ControllerBase
    {
        private readonly IBikeService _bikeService;
        private readonly IMemoryCache _cache;

        public StationsController(IBikeService bikeService, IMemoryCache cache)
        {
            _bikeService = bikeService;
            _cache = cache;
        }

        // GET: api/v1/stations
        [HttpGet]
        public async Task<IActionResult> GetStations(
            [FromQuery] string? status,
            [FromQuery] int? minBikes,
            [FromQuery] string? q,
            [FromQuery] string? sort,
            [FromQuery] string? dir,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            // Create a unique cache key based on all parameters 
            string cacheKey = $"stations_{status}_{minBikes}_{q}_{sort}_{dir}_{page}_{pageSize}";

            if (!_cache.TryGetValue(cacheKey, out PagedResponse<StationDto>? paged))
            {
                paged = await _bikeService.GetStationsAsync(status, minBikes, q, sort, dir, page, pageSize);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5)); 

                _cache.Set(cacheKey, paged, cacheEntryOptions);
            }

            return Ok(paged);
        }

        // GET: api/v1/stations/42
        [HttpGet("{number}")]
        public async Task<IActionResult> GetStation(int number)
        {
            var station = await _bikeService.GetStationByIdAsync(number);
            if (station == null) return NotFound(); // 404 if not found [cite: 11]
            return Ok(station);
        }

        // GET: api/v1/stations/summary
        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var summary = await _bikeService.GetSummaryAsync();
            return Ok(summary);
        }

        // POST and PUT stubs 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Station station)
        {
            await _bikeService.CreateStationAsync(station);
            return CreatedAtAction(nameof(GetStation), new { number = station.Number }, station);
        }

        [HttpPut("{number}")]
        public async Task<IActionResult> Update(int number, [FromBody] Station station)
        {
            await _bikeService.UpdateStationAsync(number, station);
            return NoContent();
        }
    }
}