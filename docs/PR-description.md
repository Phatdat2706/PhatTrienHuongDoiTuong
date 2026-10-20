## What
- Phân tích 5 vi phạm SOLID của `ClubManager` (`docs/solid-analysis.md`)
- Refactor thành `ClubService`, `EventService`, `EventRegistrationService`, `KioskCheckIn`, `ReportService`, phụ thuộc nhau qua interface
- Áp dụng 4 pattern: Factory (`NotificationServiceFactory`), Decorator (`LoggingClubRepository`), Strategy (`IReportStrategy`: Console, CSV), Observer (`IEventObserver`: `LogNotifier`, `EmailNotifier`)
- Thêm unit test xUnit theo Arrange-Act-Assert cho `Club`, `Event`, `Member`

## Why
`ClubManager` là God class: vừa kiểm tra, lưu, ghi log, gửi thông báo, in báo cáo, lại tự `new` class cụ thể. Tách ra giúp thêm kênh thông báo hay định dạng báo cáo mà không sửa code cũ, và test được Domain mà không cần File hay Console.

## How to test
```
dotnet build
dotnet test
dotnet run --project EduHub.Ch02
```
Demo in ra `[OK]` hoặc `[TỪ CHỐI]` cho từng tình huống, cuối cùng là báo cáo ở hai định dạng Console và CSV.
