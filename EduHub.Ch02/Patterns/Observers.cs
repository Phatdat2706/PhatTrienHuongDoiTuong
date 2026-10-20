using EduHub.Ch02.Abstractions;
using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Patterns;

// OBSERVER: EventService báo cho mọi bên đã đăng ký khi có sự kiện mới
public class LogNotifier : IEventObserver
{
    private readonly IAppLogger _logger;
    public LogNotifier(IAppLogger logger) => _logger = logger;
    public void OnEventCreated(Event ev, Club club) =>
        _logger.Info($"[observer] Sự kiện mới '{ev.Title}' của {club.Name}");
}

public class EmailNotifier : IEventObserver
{
    private readonly INotificationService _notifier;
    private readonly IMemberRepository _members;

    public EmailNotifier(INotificationService notifier, IMemberRepository members)
    {
        _notifier = notifier;
        _members = members;
    }

    public void OnEventCreated(Event ev, Club club)
    {
        foreach (var id in club.MemberIds)
        {
            var member = _members.GetById(id);
            if (member != null)
                _notifier.Send(member.Email, $"{club.Name} có sự kiện mới: {ev.Title} ({ev.Date:dd/MM/yyyy})");
        }
    }
}
