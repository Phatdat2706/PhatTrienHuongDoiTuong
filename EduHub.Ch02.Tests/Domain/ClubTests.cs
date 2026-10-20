using EduHub.Ch02.Domain;
using Xunit;

namespace EduHub.Ch02.Tests.Domain;

public class ClubTests
{
    [Fact]
    public void Create_EmptyName_Throws()
    {
        // Arrange
        string name = "   ";

        // Act
        Action act = () => Club.Create(name, "mô tả");

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void AddMember_Twice_Throws()
    {
        // Arrange
        var club = Club.Create("CLB Lập trình", "mô tả");
        var memberId = Guid.NewGuid();
        club.AddMember(memberId);

        // Act
        Action act = () => club.AddMember(memberId);

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }
}
