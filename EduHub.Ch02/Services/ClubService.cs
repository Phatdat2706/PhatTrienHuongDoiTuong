using EduHub.Ch02.Abstractions;
using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Services;

// S: chỉ điều phối nghiệp vụ CLB, từng việc phụ giao đúng chỗ
public class ClubService : IClubService
{
    private readonly IClubRepository _clubs;
    private readonly IMemberRepository _members;
    private readonly INotificationService _notifier;
    private readonly IAppLogger _logger;

    // D: nhận phụ thuộc qua interface từ bên ngoài
    public ClubService(IClubRepository clubs, IMemberRepository members,
                       INotificationService notifier, IAppLogger logger)
    {
        _clubs = clubs;
        _members = members;
        _notifier = notifier;
        _logger = logger;
    }

    public Club CreateClub(string name, string description, string presidentEmail)
    {
        var club = Club.Create(name, description);            // Domain tự kiểm tra tên
        _clubs.Add(club);                                      // repository lưu
        _logger.Info($"Club created: {name}");                 // logger ghi
        _notifier.Send(presidentEmail, "Club created");        // notifier gửi
        return club;
    }

    public void JoinClub(Guid clubId, Guid memberId)
    {
        var club = _clubs.GetById(clubId) ?? throw new InvalidOperationException("Không tìm thấy CLB");
        var member = _members.GetById(memberId) ?? throw new InvalidOperationException("Không tìm thấy thành viên");
        club.AddMember(member.Id);
        _logger.Info($"{member.FullName} đã tham gia {club.Name}");
    }
}
