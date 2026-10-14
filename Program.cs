using System.Linq;
using BaiTap1.Domain;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var lib = new Library();
var today = new DateTime(2026, 10, 14);

// ---------- Dữ liệu ban đầu ----------
var an   = new Member(1, "An");
var binh = new Member(2, "Bình");
var chi  = new Member(3, "Chi");
var dung = new Member(4, "Dũng");

var csharp = new Book("111", "Lập trình C#", 1);   // chỉ có 1 bản
var java   = new Book("222", "Java cơ bản", 5);
lib.AddBook(csharp);
lib.AddBook(java);

// ---------- Hàm hỗ trợ in kết quả ----------
void Header(string title)
{
    Console.WriteLine();
    Console.WriteLine($"=== {title} ===");
}

void Demo(string title, Action action)
{
    try
    {
        action();
        Console.WriteLine($"[OK]       {title}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[TỪ CHỐI]  {title}: {ex.Message}");
    }
}

Loan anLoan = null!;
Loan chiLoan = null!;
Loan dungLoan = null!;

// ---------- US-01: Độc giả mượn sách ----------
Header("US-01: Độc giả mượn sách");

Demo("① An mượn Java (còn bản)", () =>
{
    anLoan = lib.Borrow(an, java, today);
    Console.WriteLine($"           Loan: {anLoan.Status}, hạn trả {anLoan.DueDate:dd/MM/yyyy}, Java còn {java.Available} bản");
});

Demo("(chuẩn bị) Chi mượn C# (bản duy nhất)", () =>
{
    chiLoan = lib.Borrow(chi, csharp, today);
    Console.WriteLine($"           C# còn {csharp.Available} bản");
});

Demo("② Bình mượn C# (đã hết bản)", () => lib.Borrow(binh, csharp, today));

// ---------- US-02: Độc giả đặt trước sách ----------
Header("US-02: Độc giả đặt trước sách");

Demo("① Bình đặt trước C# (hết bản)", () =>
{
    var r = lib.Reserve(binh, csharp);
    Console.WriteLine($"           Reservation: {r.Status}");
});

Demo("② Bình đặt trước C# lần nữa (trùng)", () => lib.Reserve(binh, csharp));

// ---------- US-03: Độc giả gia hạn ----------
Header("US-03: Độc giả gia hạn sách");

Demo("① An gia hạn Java (chưa quá hạn, không ai đặt trước)", () =>
{
    var old = anLoan.DueDate;
    lib.Renew(anLoan, today);
    Console.WriteLine($"           Hạn trả: {old:dd/MM/yyyy} -> {anLoan.DueDate:dd/MM/yyyy}");
});

Demo("② Chi gia hạn C# (Bình đang đặt trước)", () => lib.Renew(chiLoan, today));

// ---------- US-04: Thủ thư nhận trả sách ----------
Header("US-04: Thủ thư nhận trả sách");

Demo("① An trả Java đúng hạn", () =>
{
    var fine = lib.ReturnBook(anLoan, today.AddDays(5));
    string fineText = fine == null ? "không phạt" : $"phạt {fine.Amount:N0}đ";
    Console.WriteLine($"           Loan: {anLoan.Status}, Java còn {java.Available} bản, {fineText}");
});

Demo("② Chi trả C# trễ 6 ngày", () =>
{
    var fine = lib.ReturnBook(chiLoan, today.AddDays(20));
    string fineText = fine == null ? "không phạt" : $"phạt {fine.Amount:N0}đ";
    Console.WriteLine($"           Loan: {chiLoan.Status}, C# còn {csharp.Available} bản, {fineText}");
});

// ---------- US-05: Thủ thư thêm đầu sách ----------
Header("US-05: Thủ thư thêm đầu sách mới");

Demo("① Thêm 'Cấu trúc dữ liệu' (ISBN mới, 3 bản)", () =>
{
    lib.AddBook(new Book("333", "Cấu trúc dữ liệu", 3));
    Console.WriteLine($"           Danh mục hiện có {lib.Books.Count} đầu sách");
});

Demo("② Thêm sách trùng ISBN 333", () =>
    lib.AddBook(new Book("333", "Sách khác", 2)));

Demo("② Thêm sách có số bản = 0", () =>
    lib.AddBook(new Book("444", "Sách rỗng", 0)));

// ---------- US-06: Admin khóa tài khoản ----------
Header("US-06: Admin khóa tài khoản độc giả");

Demo("(chuẩn bị) Dũng mượn Java", () =>
{
    dungLoan = lib.Borrow(dung, java, today);
});

Demo("① Admin khóa tài khoản Dũng", () =>
{
    lib.DeactivateMember(dung);
    Console.WriteLine($"           IsActive() = {dung.IsActive()}");
});

Demo("① Dũng (đã bị khóa) mượn thêm sách", () => lib.Borrow(dung, java, today));

Demo("② Lịch sử mượn của Dũng vẫn còn", () =>
{
    int count = lib.Loans.Count(l => l.Member == dung);
    Console.WriteLine($"           Dũng có {count} lượt mượn trong lịch sử");
});

Console.WriteLine();
Console.WriteLine("Hết demo.");