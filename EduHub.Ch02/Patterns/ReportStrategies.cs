using System.Text;
using EduHub.Ch02.Abstractions;

namespace EduHub.Ch02.Patterns;

// STRATEGY: mỗi định dạng báo cáo là một class riêng
public class ConsoleReportStrategy : IReportStrategy
{
    public string Name => "Console";
    public string Render(IReadOnlyList<ClubStat> stats)
    {
        var sb = new StringBuilder();
        foreach (var s in stats)
            sb.AppendLine($"Club: {s.ClubName} | Members: {s.Members} | Events: {s.Events}");
        return sb.ToString();
    }
}

public class CsvReportStrategy : IReportStrategy
{
    public string Name => "CSV";
    public string Render(IReadOnlyList<ClubStat> stats) =>
        "Club,Members,Events\n" + string.Join("\n", stats.Select(s => $"{Escape(s.ClubName)},{s.Members},{s.Events}"));

    private static string Escape(string v) =>
        v.Contains(',') || v.Contains('"') ? $"\"{v.Replace("\"", "\"\"")}\"" : v;
}
