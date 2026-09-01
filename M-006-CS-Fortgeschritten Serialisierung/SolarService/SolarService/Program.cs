using Microsoft.AspNetCore.Http.Json;
using SolarSystem;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace SolarService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // z.B. hier für Options: https://code-maze.com/aspnetcore-set-global-default-json-serialization-options/
        builder.Services.Configure<JsonOptions>(o =>
        {
            o.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        });
        builder.Services.AddScoped<CelestialService>();

        var app = builder.Build();


        app.MapGet("/", (HttpContext httpContext) =>
        {
            return "Hello world";
        });

        app.MapGet("/status", (HttpContext httpContext) =>
        {
            return "Healthy";
        });

        app.MapGet("/SolarSystem", (CelestialService service) =>
        {
            var result = service.GetSolarSystem();
            return result;
        });

        app.MapPost("/SolarSystem", (CelestialService service, Node<CelestialBody> data) =>
        {
            service.SaveSolarSystem(data);
            return Results.Ok();
        });

        app.Run();
    }
}

public class CelestialService
{
    public Node<CelestialBody> GetSolarSystem()
    {
        var result = SolarSystem.Program.CreateSolarSystem();
        return result;
    }

    public void SaveSolarSystem(Node<CelestialBody> data)
    {
        Trace.WriteLine("data saved");
    }
}
