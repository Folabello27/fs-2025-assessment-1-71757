using Asp.Versioning;
using fs_2025_assessment_1_71757.Services;
using fs_2025_assessment_1_71757.BackgroundServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();


builder.Services.AddSingleton<IBikeService, BikeService>();


builder.Services.AddHostedService<StationUpdateService>();

builder.Services.AddControllers();


builder.Services.AddApiVersioning(options =>
    {
    options.DefaultApiVersion = new ApiVersion(1, 0);
options.AssumeDefaultVersionWhenUnspecified = true;
options.ReportApiVersions = true;
}).AddMvc();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();