namespace EduHub.Ch02.Domain;

public class Event
{
    public Guid Id { get; private set; }
    public Guid ClubId { get; private set; }
    public string Title { get; private set; }
    public DateTime Date { get; private set; }

    private readonly List<Guid> _registered = new();
    private readonly HashSet<Guid> _checkedIn = new();
    public IReadOnlyList<Guid> RegisteredMemberIds => _registered;
    public int CheckedInCount => _checkedIn.Count;

    private Event(Guid clubId, string title, DateTime date)
    {
        Id = Guid.NewGuid();
        ClubId = clubId;
        Title = title;
        Date = date;
    }

    public static Event Create(Guid clubId, string title, DateTime date)
    {
        if (clubId == Guid.Empty)
            throw new ArgumentException("ClubId không hợp lệ");
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Tiêu đề sự kiện không được trống");
        if (date == default)
            throw new ArgumentException("Ngày diễn ra không hợp lệ");
        return new Event(clubId, title.Trim(), date);
    }

    public bool IsCheckedIn(Guid memberId) => _checkedIn.Contains(memberId);

    public void Register(Guid memberId)
    {
        if (_registered.Contains(memberId))
            throw new InvalidOperationException("Đã đăng ký sự kiện này rồi");
        _registered.Add(memberId);
    }

    public void CheckIn(Guid memberId)
    {
        if (!_registered.Contains(memberId))
            throw new InvalidOperationException("Chưa đăng ký nên không check-in được");
        if (!_checkedIn.Add(memberId))
            throw new InvalidOperationException("Đã check-in rồi");
    }
}
