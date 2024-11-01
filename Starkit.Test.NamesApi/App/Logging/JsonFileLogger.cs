using System.Text.Json;

public class JsonFileLogger : ILogger
{
    private readonly string _filePath;

    public JsonFileLogger(string filePath)
    {
        _filePath = filePath;
    }

    public IDisposable BeginScope<TState>(TState state) => null;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;

        var logEntry = new
        {
            Date = DateTime.UtcNow,
            Level = logLevel.ToString(),
            Message = formatter(state, exception),
            Exception = exception?.ToString()
        };

        var logContent = JsonSerializer.Serialize(logEntry) + Environment.NewLine;

        File.AppendAllText(_filePath, logContent);
    }
}
