namespace BaiTap1.Domain;

public class Member
{
    public const int MaxLoans = 3;

    public int Id { get; }
    public string Name { get; }
    private bool _active = true;

    public Member(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public bool IsActive() => _active;

    // Khóa tài khoản. Lịch sử mượn và phạt vẫn được giữ ở Library.
    public void Deactivate() => _active = false;
}