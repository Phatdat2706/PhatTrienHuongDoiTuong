namespace EduHub.Ch02.Domain;

public class Club
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    private readonly List<Guid> _memberIds = new();
    public IReadOnlyList<Guid> MemberIds => _memberIds;

    private Club(string name, string description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
    }

    // Domain tự kiểm tra tên
    public static Club Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên CLB không được trống");
        if (name.Length > 100)
            throw new ArgumentException("Tên CLB tối đa 100 ký tự");
        return new Club(name.Trim(), description ?? string.Empty);
    }

    public void AddMember(Guid memberId)
    {
        if (_memberIds.Contains(memberId))
            throw new InvalidOperationException("Đã là thành viên của CLB");
        _memberIds.Add(memberId);
    }
}
