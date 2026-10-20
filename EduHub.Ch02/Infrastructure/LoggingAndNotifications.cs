using EduHub.Ch02.Abstractions;

namespace EduHub.Ch02.Infrastructure;

public class ConsoleLogger : IAppLogger
{
    public void Info(string message) => Console.WriteLine($"[LOG] {message}");
}

// Ghi log ra file thay vì Console: chỉ cần đổi một dòng ở Program.cs
public class FileLogger : IAppLogger
{
    private readonly string _path;
    public FileLogger(string path) => _path = path;
    public void Info(string message) => File.AppendAllText(_path, message + Environment.NewLine);
}

public class EmailNotificationService : INotificationService
{
    public void Send(string to, string msg) => Console.WriteLine($"[EMAIL -> {to}] {msg}");
}

public class SmsNotificationService : INotificationService
{
    public void Send(string to, string msg) => Console.WriteLine($"[SMS -> {to}] {msg}");
}

public class SlackNotificationService : INotificationService
{
    public void Send(string to, string msg) => Console.WriteLine($"[SLACK -> {to}] {msg}");
}
