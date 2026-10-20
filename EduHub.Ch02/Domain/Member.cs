namespace EduHub.Ch02.Domain;

public class Member
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }

    private Member(string fullName, string email)
    {
        Id = Guid.NewGuid();
        FullName = fullName;
        Email = email;
    }

    public static Member Create(string fullName, string email)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Họ tên không được trống");
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("Email không hợp lệ");
        return new Member(fullName.Trim(), email.Trim());
    }
}
