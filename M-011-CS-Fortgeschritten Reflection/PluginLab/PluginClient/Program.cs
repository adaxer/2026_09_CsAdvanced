using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PluginBase;
using System.Reflection;

namespace PluginClient;

internal class Program
{
    static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

       builder.Services.AddSingleton<App>();
       builder.Services.AddTransient<PluginLoader>();

        using var host = builder.Build();

        await host.Services.GetRequiredService<App>().RunAsync(); 
    }
}

internal class PluginLoader
{
    private readonly IConfiguration _configuration;


    public PluginLoader(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    internal async Task<IEnumerable<IPlugin>> LoadPluginsAsync()
    {
        var plugins = _configuration["Plugins"]?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
            var result = new List<IPlugin>();
        foreach (var plugin in plugins)
        {
            if(File.Exists(plugin))
            {
            Console.WriteLine($"Loading plugin: {plugin}");
                var info = new FileInfo(plugin);
                var assembly = Assembly.LoadFile(info.FullName);
                foreach (var pluginType in assembly.GetTypes().Where(t=>typeof(IPlugin).IsAssignableFrom(t)))
                {
                    Console.WriteLine($"Found plugin type: {pluginType.FullName}");
                    result.Add((IPlugin)Activator.CreateInstance(pluginType));
                }
            }
        }
        return result;
    }
}

internal class App
{
    private readonly PluginLoader _pluginLoader;
    private IEnumerable<IPlugin> _plugins = Enumerable.Empty<IPlugin>();

    public App(PluginLoader pluginLoader)
    {
        _pluginLoader = pluginLoader;
    }

    public async Task RunAsync()
    {
        Console.WriteLine("Loading Plugins ...");
        _plugins = await _pluginLoader.LoadPluginsAsync();
        foreach (var plugin in _plugins)
        {
            await plugin.InitializeAsync("PluginClient");
            await plugin.ExecuteAsync("Test Input");
        }

        Console.WriteLine("Plugins loaded. Select Plugin(s) to call");

    }
}