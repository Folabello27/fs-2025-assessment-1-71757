using fs_2025_api_20250925_71757.Data;

namespace fs_2025_api_20250925_71757.Startup
{
    public static class DependenciesConfig
    {
        public static void AddDependencies(this WebApplicationBuilder builder)
        {
            // Cada vez que se pide CourseData, crea una nueva instancia
            builder.Services.AddTransient<CourseData>();
        }
    }
}