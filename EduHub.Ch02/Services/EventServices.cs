using EduHub.Ch02.Abstractions;
using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Services;

// EventService là "người phát tin" của Observer
public class EventService : IEventPlanner, IEventSubject
{
    private readonly IClubReader _clubs;          // I: chỉ cần đọc CLB
    private readonly IEventRepository _events;
    private readonly IAppLogger _logger;
    private readonly List<IEventObserver> _observers = new();

    public EventService(IClubReader clubs, IEventRepository events, IAppLogger logger)
    {
        _clubs = clubs;
        _events = events;
        _logger = logger;
    }

    public void Subscribe(IEventObserver observer) => _observers.Add(observer);
    public void Unsubscribe(IEventObserver observer) => _observers.Remove(observer);

    public Event CreateEvent(Guid clubId, string title, DateTime date)
    {
        var club = _clubs.GetById(clubId) ?? throw new InvalidOperationException("Không tìm thấy CLB");
        var ev = Event.Create(clubId, title, date);
        _events.Add(ev);
        _logger.Info($"Đã tạo sự kiện '{ev.Title}'");
        foreach (var o in _observers.ToList()) o.OnEventCreated(ev, club);
        return ev;
    }
}

public class EventRegistrationService : IEventRegistration
{
    private readonly IEventRepository _events;
    private readonly IMemberRepository _members;
    private readonly IAppLogger _logger;

    public EventRegistrationService(IEventRepository events, IMemberRepository members, IAppLogger logger)
    {
        _events = events;
        _members = members;
        _logger = logger;
    }

    public void Register(Guid eventId, Guid memberId)
    {
        var ev = _events.GetById(eventId) ?? throw new InvalidOperationException("Không tìm thấy sự kiện");
        var member = _members.GetById(memberId) ?? throw new InvalidOperationException("Không tìm thấy thành viên");
        ev.Register(member.Id);
        _logger.Info($"{member.FullName} đã đăng ký '{ev.Title}'");
    }
}

// I: máy check-in chỉ ký IEventCheckIn
public class KioskCheckIn : IEventCheckIn
{
    private readonly IEventRepository _events;
    private readonly IAppLogger _logger;

    public KioskCheckIn(IEventRepository events, IAppLogger logger)
    {
        _events = events;
        _logger = logger;
    }

    public void CheckIn(Guid eventId, Guid memberId)
    {
        var ev = _events.GetById(eventId) ?? throw new InvalidOperationException("Không tìm thấy sự kiện");
        ev.CheckIn(memberId);
        _logger.Info($"Check-in thành công tại '{ev.Title}'");
    }
}

public class ReportService
{
    private readonly IClubReader _clubs;
    private readonly IEventRepository _events;

    public ReportService(IClubReader clubs, IEventRepository events)
    {
        _clubs = clubs;
        _events = events;
    }

    // Không biết định dạng: strategy tự render
    public string Generate(IReportStrategy strategy)
    {
        var stats = _clubs.GetAll()
            .Select(c => new ClubStat(c.Name, c.MemberIds.Count, _events.GetByClub(c.Id).Count))
            .ToList();
        return strategy.Render(stats);
    }
}
