using PluginBase;

namespace WebLoader;

public class WebLoader : IPlugin
{
    public Task<object?> ExecuteAsync<T>(T? input)
    {
        Console.WriteLine($"Executing with input: {input}");
        return Task.FromResult<object?>($"Url {input} verarbeitet und geladen");
    }

    public Task InitializeAsync<T>(T? context)
    {
        Console.WriteLine($"Context {context} initialisiert");
        return Task.CompletedTask;
    }
}
