using ApiService.Utils;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var apiVersionString = ApiSpecReader.ReadApiVersion();
var majorVersion = apiVersionString.Split('.')[0];
var apiVersion = $"v{majorVersion}";

// Add Swagger with correct version
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc(apiVersion, new OpenApiInfo {
        Title = "Items API",
        Version = apiVersion
    });
});

var app = builder.Build();

app.UseSwagger();

var swaggerPath = $"/swagger/{apiVersion}/swagger.json";
var serviceTitle = $"ApiService {apiVersion}";
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint(swaggerPath, serviceTitle);
});

app.MapGet("/", () => "Hello World!");

app.Run();
