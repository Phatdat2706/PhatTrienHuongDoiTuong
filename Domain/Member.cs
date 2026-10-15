namespace BaiTap1.Domain;

public class Member
{
    public const int MaxLoans = 3;

    public int Id { get; private set; }
    public string Name { get; private set; }
    private bool _active = true;

    private Member(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Member Create(int id, string name)
    {
        if (id <= 0)
            throw new ArgumentException("Id phải > 0");
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên trống");
        return new Member(id, name);
    }

    public bool IsActive() => _active;

    public void Deactivate() => _active = false;
}