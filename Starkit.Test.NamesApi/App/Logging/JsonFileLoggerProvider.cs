public class JsonFileLoggerProvider : ILoggerProvider
{
    private readonly string _filePath;

    public JsonFileLoggerProvider(string filePath)
    {
        _filePath = filePath;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new JsonFileLogger(_filePath);
    }

    public void Dispose() { }
}
