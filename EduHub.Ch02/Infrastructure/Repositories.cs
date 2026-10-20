using EduHub.Ch02.Abstractions;
using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Infrastructure;

public class InMemoryClubRepository : IClubRepository
{
    private readonly List<Club> _clubs = new();
    public void Add(Club club) => _clubs.Add(club);
    public IReadOnlyList<Club> GetAll() => _clubs.AsReadOnly();
    public Club? GetById(Guid id) => _clubs.FirstOrDefault(c => c.Id == id);
}

public class InMemoryMemberRepository : IMemberRepository
{
    private readonly List<Member> _members = new();
    public void Add(Member member) => _members.Add(member);
    public Member? GetById(Guid id) => _members.FirstOrDefault(m => m.Id == id);
}

public class InMemoryEventRepository : IEventRepository
{
    private readonly List<Event> _events = new();
    public void Add(Event ev) => _events.Add(ev);
    public Event? GetById(Guid id) => _events.FirstOrDefault(e => e.Id == id);
    public IReadOnlyList<Event> GetByClub(Guid clubId) => _events.Where(e => e.ClubId == clubId).ToList();
}
