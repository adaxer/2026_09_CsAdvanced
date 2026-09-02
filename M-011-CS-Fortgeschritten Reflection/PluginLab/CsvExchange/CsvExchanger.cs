using PluginBase;

namespace CsvExchange;

public class CsvExchanger : IPlugin
{
    public Task<object?> ExecuteAsync<T>(T? input)
    {
        Console.WriteLine($"Executing with input: {input}");
        return Task.FromResult<object?>($"Input {input} verarbeitet und als CSV gespeichert");
    }

    public Task InitializeAsync<T>(T? context)
    {
        Console.WriteLine($"Context {context} initialisiert");
        return Task.CompletedTask;
    }
}
