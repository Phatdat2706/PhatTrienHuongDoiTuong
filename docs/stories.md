# User Stories - BookNest (hệ thống quản lý thư viện trường)

## Vai trò
- **Độc giả (sinh viên):** US-01 → US-03
- **Thủ thư:** US-04 → US-05
- **Admin:** US-06

---

## US-01 · Độc giả: mượn sách
- ① Sách còn bản (Available > 0) và đang mượn dưới 3 cuốn ➜ mượn ➜ tạo Loan (Borrowed), Available giảm 1
- ② Sách hết bản hoặc đã mượn đủ 3 cuốn ➜ mượn ➜ bị từ chối, báo rõ lý do

## US-02 · Độc giả: đặt trước sách
- ① Sách đã hết bản ➜ đặt trước ➜ tạo Reservation (Waiting), vào hàng chờ
- ② Đã đặt trước đúng cuốn đó rồi ➜ đặt lại ➜ bị từ chối (không trùng)

## US-03 · Độc giả: gia hạn sách đang mượn
- ① Loan chưa quá hạn, chưa gia hạn lần nào, không ai đặt trước ➜ gia hạn ➜ DueDate cộng thêm 7 ngày
- ② Loan đã quá hạn hoặc đang có người đặt trước ➜ gia hạn ➜ bị từ chối

## US-04 · Thủ thư: nhận trả sách
- ① Trả đúng hạn ➜ nhận trả ➜ Loan chuyển Returned, Available tăng 1
- ② Trả trễ ➜ nhận trả ➜ Loan chuyển Returned và tạo Fine = số ngày trễ × 2.000đ

## US-05 · Thủ thư: thêm đầu sách mới
- ① Có tiêu đề, ISBN chưa tồn tại, số bản > 0 ➜ thêm ➜ sách xuất hiện trong danh mục
- ② ISBN đã tồn tại hoặc số bản ≤ 0 ➜ thêm ➜ báo lỗi

## US-06 · Admin: khóa tài khoản độc giả
- ① Độc giả đang hoạt động (Active) ➜ khóa ➜ IsActive() = false, không mượn thêm được
- ② Lịch sử mượn và các khoản phạt của người đó vẫn được giữ