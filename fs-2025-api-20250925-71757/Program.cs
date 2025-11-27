using fs_2025_api_20250925_71757.Endpoints;
using fs_2025_api_20250925_71757.Startup;

var builder = WebApplication.CreateBuilder(args);

// registrar dependencias
builder.AddDependencies();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Endpoints
app.AddWeatherEndPoints();
app.AddCourseEndPoints();
app.AddRootEndPoints();

app.Run();