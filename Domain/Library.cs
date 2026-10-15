using System.Linq;

namespace BaiTap1.Domain;

public class Library
{
    public List<Book> Books { get; } = new();
    public List<Loan> Loans { get; } = new();
    public List<Reservation> Reservations { get; } = new();
    public List<Fine> Fines { get; } = new();

    // US-05: thêm đầu sách mới
    public void AddBook(Book book)
    {
        if (Books.Any(b => b.Isbn == book.Isbn))
            throw new InvalidOperationException("ISBN đã tồn tại");
        Books.Add(book);
    }

    // US-01: mượn sách
    public Loan Borrow(Member member, Book book, DateTime today)
    {
        if (!member.IsActive())
            throw new InvalidOperationException("Tài khoản đã bị khóa");

        int current = Loans.Count(l => l.Member == member && l.Status == LoanStatus.Borrowed);
        if (current >= Member.MaxLoans)
            throw new InvalidOperationException("Đã mượn đủ 3 cuốn");

        book.TakeOne(); // tự báo lỗi nếu hết bản

        var loan = Loan.Create(member, book, today);
        Loans.Add(loan);
        return loan;
    }

    // US-02: đặt trước sách
    public Reservation Reserve(Member member, Book book)
    {
        if (book.Available > 0)
            throw new InvalidOperationException("Sách còn bản, hãy mượn trực tiếp");
        if (Reservations.Any(r => r.Member == member && r.Book == book))
            throw new InvalidOperationException("Bạn đã đặt trước cuốn này rồi");

        var reservation = Reservation.Create(member, book);
        Reservations.Add(reservation);
        return reservation;
    }

    // US-03: gia hạn sách đang mượn
    public void Renew(Loan loan, DateTime today)
    {
        bool hasReservation = Reservations.Any(r =>
            r.Book == loan.Book && r.Member != loan.Member);
        loan.Renew(today, hasReservation);
    }

    // US-04: nhận trả sách (trả về Fine nếu trễ, null nếu đúng hạn)
    public Fine? ReturnBook(Loan loan, DateTime today)
    {
        Fine? fine = loan.Return(today);
        if (fine != null)
            Fines.Add(fine);
        return fine;
    }

    // US-06: khóa tài khoản độc giả (Loans và Fines vẫn giữ nguyên)
    public void DeactivateMember(Member member) => member.Deactivate();
}