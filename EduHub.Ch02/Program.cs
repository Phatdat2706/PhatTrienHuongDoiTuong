using EduHub.Ch02.Abstractions;
using EduHub.Ch02.Domain;
using EduHub.Ch02.Infrastructure;
using EduHub.Ch02.Patterns;
using EduHub.Ch02.Services;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// ===== Composition root: nơi DUY NHẤT được `new` class cụ thể =====
IAppLogger logger = new ConsoleLogger();                      // đổi sang FileLogger: sửa đúng dòng này
INotificationServiceFactory factory = new NotificationServiceFactory();   // Factory
INotificationService notifier = factory.Create(NotificationChannel.Email);

IClubRepository clubRepo =
    new LoggingClubRepository(new InMemoryClubRepository(), logger);      // Decorator
IMemberRepository memberRepo = new InMemoryMemberRepository();
IEventRepository eventRepo = new InMemoryEventRepository();

IClubService clubService = new ClubService(clubRepo, memberRepo, notifier, logger);
var eventService = new EventService(clubRepo, eventRepo, logger);
eventService.Subscribe(new LogNotifier(logger));                           // Observer
eventService.Subscribe(new EmailNotifier(notifier, memberRepo));
IEventRegistration registration = new EventRegistrationService(eventRepo, memberRepo, logger);
IEventCheckIn checkIn = new KioskCheckIn(eventRepo, logger);
var reportService = new ReportService(clubRepo, eventRepo);

void Try(string title, Action action)
{
    try { action(); Console.WriteLine($"[OK]       {title}"); }
    catch (Exception ex) { Console.WriteLine($"[TỪ CHỐI]  {title}: {ex.Message}"); }
}

// ===== Demo =====
var an = Member.Create("An", "an@student.dthu.edu.vn");
var binh = Member.Create("Bình", "binh@student.dthu.edu.vn");
memberRepo.Add(an); memberRepo.Add(binh);

Club club = null!;
Event ev = null!;

Try("Tạo CLB Lập trình", () => club = clubService.CreateClub("CLB Lập trình", "Học và chia sẻ code", "chunhiem@dthu.edu.vn"));
Try("Tạo CLB tên trống", () => clubService.CreateClub("  ", "x", "chunhiem@dthu.edu.vn"));
Try("An tham gia CLB", () => clubService.JoinClub(club.Id, an.Id));
Try("Bình tham gia CLB", () => clubService.JoinClub(club.Id, binh.Id));
Try("An tham gia lại", () => clubService.JoinClub(club.Id, an.Id));

Try("Tạo sự kiện Workshop SOLID", () => ev = eventService.CreateEvent(club.Id, "Workshop SOLID", new DateTime(2026, 11, 5)));
Try("Tạo sự kiện tiêu đề trống", () => eventService.CreateEvent(club.Id, "", new DateTime(2026, 11, 5)));

Try("An đăng ký", () => registration.Register(ev.Id, an.Id));
Try("An đăng ký lại", () => registration.Register(ev.Id, an.Id));
Try("An check-in", () => checkIn.CheckIn(ev.Id, an.Id));
Try("Bình check-in (chưa đăng ký)", () => checkIn.CheckIn(ev.Id, binh.Id));

Console.WriteLine();
Console.WriteLine("--- Báo cáo (Strategy: Console) ---");
Console.Write(reportService.Generate(new ConsoleReportStrategy()));
Console.WriteLine("--- Báo cáo (Strategy: CSV) ---");
Console.WriteLine(reportService.Generate(new CsvReportStrategy()));
