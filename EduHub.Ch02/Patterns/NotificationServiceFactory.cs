using EduHub.Ch02.Abstractions;
using EduHub.Ch02.Infrastructure;

namespace EduHub.Ch02.Patterns;

// FACTORY: nơi gọi chỉ nói cần kênh nào, nhà máy tự tạo
public class NotificationServiceFactory : INotificationServiceFactory
{
    public INotificationService Create(NotificationChannel channel) => channel switch
    {
        NotificationChannel.Email => new EmailNotificationService(),
        NotificationChannel.Sms => new SmsNotificationService(),
        NotificationChannel.Slack => new SlackNotificationService(),
        _ => throw new ArgumentOutOfRangeException(nameof(channel))
    };
}
