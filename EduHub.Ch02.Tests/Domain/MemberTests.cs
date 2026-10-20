using EduHub.Ch02.Domain;
using Xunit;

namespace EduHub.Ch02.Tests.Domain;

public class MemberTests
{
    [Theory]
    [InlineData("")]
    [InlineData("khong-co-a-cong")]
    public void Create_InvalidEmail_Throws(string email)
    {
        // Arrange + Act
        Action act = () => Member.Create("An", email);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Create_ValidData_TrimsName()
    {
        // Arrange + Act
        var member = Member.Create("  An  ", "an@student.dthu.edu.vn");

        // Assert
        Assert.Equal("An", member.FullName);
    }
}
