using EduHub.Ch02.Domain;
using Xunit;

namespace EduHub.Ch02.Tests.Domain;

public class EventTests
{
    [Fact]
    public void Create_EmptyTitle_Throws()
    {
        // Arrange
        var clubId = Guid.NewGuid();

        // Act
        Action act = () => Event.Create(clubId, "", new DateTime(2026, 11, 5));

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Register_Twice_Throws()
    {
        // Arrange
        var ev = Event.Create(Guid.NewGuid(), "Workshop", new DateTime(2026, 11, 5));
        var memberId = Guid.NewGuid();
        ev.Register(memberId);

        // Act
        Action act = () => ev.Register(memberId);

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void CheckIn_NotRegistered_Throws()
    {
        // Arrange
        var ev = Event.Create(Guid.NewGuid(), "Workshop", new DateTime(2026, 11, 5));
        var memberId = Guid.NewGuid();

        // Act
        Action act = () => ev.CheckIn(memberId);

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void CheckIn_Registered_IncreasesCheckedInCount()
    {
        // Arrange
        var ev = Event.Create(Guid.NewGuid(), "Workshop", new DateTime(2026, 11, 5));
        var memberId = Guid.NewGuid();
        ev.Register(memberId);

        // Act
        ev.CheckIn(memberId);

        // Assert
        Assert.Equal(1, ev.CheckedInCount);
        Assert.True(ev.IsCheckedIn(memberId));
    }
}
