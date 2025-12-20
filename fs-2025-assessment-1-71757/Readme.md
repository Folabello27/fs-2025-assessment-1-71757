
# Dublin Bikes API Assessment (V1)

**Repository:** `fs-2025-assessment-1-<student-number>`

This project implements a versioned .NET 8 Web API for the Dublin Bikes data, featuring in-memory caching and a real-time data simulation background service.

## 1. Setup & Execution 

1.  **Prerequisites:** Ensure **.NET 8 SDK** is installed.
2.  **Data Placement:** Place `dublinbike.json` into the **`Data/`** directory.
3.  **Run:** Start the application via your IDE (Rider/Visual Studio) or by running `dotnet run` in the terminal.
4.  The API runs on `https://localhost:<PortNumber>` (e.g., `/api/v1/`).

## 2. Sample API Calls 

The primary endpoint is `GET /api/v1/stations`.

| Requirement | Example Query | Notes |
| :--- | :--- | :--- |
| **Search & Paging** | `?q=square&page=1&pageSize=5` | Searches Name/Address, returns 5 results. |
| **Filter & Sort** | `?status=OPEN&minBikes=10&sort=occupancy&dir=desc` | Filters by status and bike count, then sorts by calculated `Occupancy`. |
| **Summary** | `GET /api/v1/stations/summary` | Returns aggregate counts of stations, stands, and availability. |

## 3. Design Choices 

| Component | Design Choice | Rationale |
| :--- | :--- | :--- |
| **Data Store** | **Singleton** `BikeService` | Ensures all requests and the background task share the single, mutable **in-memory** `List<Station>` object. |
| **Simulation** | **`BackgroundService`** | Runs on a separate, non-blocking thread to update `availableBikes` every **10-20 seconds**, simulating a live data feed. |
| **Performance** | **`IMemoryCache`** | Implemented in the Controller to cache all `GET /stations` query results for **5 minutes**, reducing redundant processing and improving responsiveness. |
| **Logic** | **Separation of Concerns** | All complex filtering, sorting (LINQ), time conversion, and occupancy calculations are confined to the `BikeService` layer. |