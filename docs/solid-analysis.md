# Nhiệm vụ 1: Phân tích vi phạm SOLID trong ClubManager

Nguồn đối chiếu: slide Chương 2 mục 2.1 (hình 2.1, bảng 2.1) và code `ClubManager.cs` trong starter.
Trước khi nộp, mở `ClubManager.cs` và điền số dòng thật vào cột "Vị trí".

| # | Nguyên lý | Vị trí (điền dòng) | Vi phạm | Hậu quả | Cách sửa trong bài |
|---|---|---|---|---|---|
| 1 | **S** - Single Responsibility | `ClubManager.cs`, dòng 18 (`CreateClub`) | `CreateClub` vừa kiểm tra tên, vừa lưu CLB, vừa ghi log ra file, vừa báo tin; cả class ôm 5 việc: kiểm tra, lưu, log, thông báo, in báo cáo | Class có 4-5 lý do để sửa; khó đọc, khó kiểm thử | `Club.Create` kiểm tra; `IClubRepository` lưu; `IAppLogger` ghi log; `INotificationService` gửi tin; `ReportService` làm báo cáo; `ClubService` chỉ điều phối |
| 2 | **O** - Open/Closed | `ClubManager.cs`, dòng 31–36 (chuỗi `if/else` theo `@gmail.com`, `@yahoo.com`) | Chọn cách gửi thông báo bằng `if/else` theo đuôi email | Thêm SMS hay Slack phải mở method đang chạy tốt ra sửa, dễ sinh lỗi mới | `INotificationService` + mỗi kênh một class (`Email`, `Sms`, `Slack`); `NotificationServiceFactory` chọn kênh theo `NotificationChannel` |
| 3 | **L** - Liskov Substitution | `ClubManager.cs`, dòng 13–15 (kho dữ liệu dùng chung) | Một kho dữ liệu ôm mọi thao tác; bản chỉ đọc vẫn phải "hứa" có `Add` rồi ném `NotSupportedException` | Code đang gọi `repo.Add(club)` sập khi gặp bản chỉ đọc | `IClubReader` (đọc) và `IClubRepository : IClubReader` (thêm `Add`); bản chỉ đọc chỉ ký `IClubReader` |
| 4 | **I** - Interface Segregation | `ClubManager.cs`, dòng 10–101 (interface/service ôm mọi thao tác) | Một interface lớn gồm tạo sự kiện, đăng ký, check-in, xuất báo cáo | Máy check-in bị ép cài cả method không dùng (`NotImplementedException`) | `IEventPlanner`, `IEventRegistration`, `IEventCheckIn`; `KioskCheckIn` chỉ ký `IEventCheckIn` |
| 5 | **D** - Dependency Inversion | `ClubManager.cs`, dòng 27–28 (`File.AppendAllText`, `Console.WriteLine`, `new` bên trong) | Logic gọi thẳng `File`, `Console` và tự `new` class cụ thể | Không đổi được nơi ghi log/lưu dữ liệu; không viết được test với bản giả | Gọi qua `IAppLogger`, `INotificationService`, `IClubRepository` nhận từ constructor; `Program.cs` là nơi duy nhất `new` |

Đề yêu cầu ít nhất 4 vi phạm; bảng có 5 dòng, giữ dòng nào thấy rõ trong code của mình.
