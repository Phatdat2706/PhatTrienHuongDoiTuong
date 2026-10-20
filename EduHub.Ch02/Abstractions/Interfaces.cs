using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Abstractions;

public interface IAppLogger { void Info(string message); }

// O: thêm kênh mới chỉ cần thêm class mới
public interface INotificationService { void Send(string to, string msg); }

// Factory
public enum NotificationChannel { Email, Sms, Slack }
public interface INotificationServiceFactory { INotificationService Create(NotificationChannel channel); }

// L + I: tách đọc và ghi, không có class "hứa mà không làm được"
public interface IClubReader
{
    IReadOnlyList<Club> GetAll();
    Club? GetById(Guid id);
}
public interface IClubRepository : IClubReader { void Add(Club club); }

public interface IMemberRepository
{
    void Add(Member member);
    Member? GetById(Guid id);
}

public interface IEventRepository
{
    void Add(Event ev);
    Event? GetById(Guid id);
    IReadOnlyList<Event> GetByClub(Guid clubId);
}

// I: interface nhỏ, ai cần gì ký nấy
public interface IClubService
{
    Club CreateClub(string name, string description, string presidentEmail);
    void JoinClub(Guid clubId, Guid memberId);
}
public interface IEventPlanner { Event CreateEvent(Guid clubId, string title, DateTime date); }
public interface IEventRegistration { void Register(Guid eventId, Guid memberId); }
public interface IEventCheckIn { void CheckIn(Guid eventId, Guid memberId); }

// Observer
public interface IEventObserver { void OnEventCreated(Event ev, Club club); }
public interface IEventSubject
{
    void Subscribe(IEventObserver observer);
    void Unsubscribe(IEventObserver observer);
}

// Strategy
public record ClubStat(string ClubName, int Members, int Events);
public interface IReportStrategy
{
    string Name { get; }
    string Render(IReadOnlyList<ClubStat> stats);
}
