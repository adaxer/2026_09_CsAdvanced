namespace PluginBase;

public interface IPlugin
{
    Task InitializeAsync<T>(T? context);

    Task<object?> ExecuteAsync<T>(T? input);
}
