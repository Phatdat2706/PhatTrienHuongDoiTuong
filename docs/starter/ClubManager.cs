using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// Starter code – "God class" vi phạm SOLID.
/// Dùng để đối chiếu với bảng phân tích docs/solid-analysis.md.
/// </summary>
public class ClubManager
{
    // --- Kho dữ liệu dùng chung (vi phạm L & I) ---
    private readonly List<Club> _clubs = new();
    private readonly List<Member> _members = new();
    private readonly List<Event> _events = new();

    // === S vi phạm: CreateClub vừa kiểm tra, vừa lưu, vừa ghi log, vừa gửi mail ===
    public Club CreateClub(string name, string description, string presidentEmail)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên CLB không được để trống.");

        var club = new Club { Id = Guid.NewGuid(), Name = name.Trim(), Description = description };
        _clubs.Add(club);

        // D vi phạm: gọi thẳng File & Console, tự new bên trong
        File.AppendAllText("log.txt", $"[{DateTime.Now}] Tạo CLB: {club.Name}\n");
        Console.WriteLine($"[LOG] Tạo CLB: {club.Name}");

        // O vi phạm: if/else theo đuôi email
        if (presidentEmail.EndsWith("@gmail.com"))
            Console.WriteLine($"[EMAIL] Gửi Gmail tới {presidentEmail}");
        else if (presidentEmail.EndsWith("@yahoo.com"))
            Console.WriteLine($"[EMAIL] Gửi Yahoo tới {presidentEmail}");
        else
            Console.WriteLine($"[EMAIL] Gửi SMTP mặc định tới {presidentEmail}");

        return club;
    }

    public void JoinClub(Guid clubId, Guid memberId)
    {
        var club = _clubs.FirstOrDefault(c => c.Id == clubId)
            ?? throw new InvalidOperationException("CLB không tồn tại.");
        var member = _members.FirstOrDefault(m => m.Id == memberId)
            ?? throw new InvalidOperationException("Thành viên không tồn tại.");

        if (club.MemberIds.Contains(memberId))
            throw new InvalidOperationException("Thành viên đã ở trong CLB.");

        club.MemberIds.Add(memberId);
        File.AppendAllText("log.txt", $"[{DateTime.Now}] {member.Name} tham gia {club.Name}\n");
    }

    // I vi phạm: tạo sự kiện, đăng ký, check-in, xuất báo cáo đều trong một class
    public Event CreateEvent(Guid clubId, string title, DateTime date)
    {
        var club = _clubs.FirstOrDefault(c => c.Id == clubId)
            ?? throw new InvalidOperationException("CLB không tồn tại.");

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Tiêu đề sự kiện không được để trống.");

        var ev = new Event { Id = Guid.NewGuid(), ClubId = clubId, Title = title, Date = date };
        _events.Add(ev);
        Console.WriteLine($"[LOG] Tạo sự kiện: {title}");
        return ev;
    }

    public void Register(Guid eventId, Guid memberId)
    {
        var ev = _events.FirstOrDefault(e => e.Id == eventId)
            ?? throw new InvalidOperationException("Sự kiện không tồn tại.");

        if (ev.RegisteredIds.Contains(memberId))
            throw new InvalidOperationException("Đã đăng ký rồi.");

        ev.RegisteredIds.Add(memberId);
    }

    public void CheckIn(Guid eventId, Guid memberId)
    {
        var ev = _events.FirstOrDefault(e => e.Id == eventId)
            ?? throw new InvalidOperationException("Sự kiện không tồn tại.");

        if (!ev.RegisteredIds.Contains(memberId))
            throw new InvalidOperationException("Chưa đăng ký, không thể check-in.");

        ev.CheckedInIds.Add(memberId);
    }

    public string GenerateReport()
    {
        var lines = new List<string> { "CLB,Thành viên,Sự kiện" };
        foreach (var club in _clubs)
        {
            int events = _events.Count(e => e.ClubId == club.Id);
            lines.Add($"{club.Name},{club.MemberIds.Count},{events}");
        }
        return string.Join("\n", lines);
    }
}

// --- Các class phụ đơn giản ---
public class Club
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public List<Guid> MemberIds { get; set; } = new();
}

public class Member
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
}

public class Event
{
    public Guid Id { get; set; }
    public Guid ClubId { get; set; }
    public string Title { get; set; } = "";
    public DateTime Date { get; set; }
    public List<Guid> RegisteredIds { get; set; } = new();
    public List<Guid> CheckedInIds { get; set; } = new();
}
