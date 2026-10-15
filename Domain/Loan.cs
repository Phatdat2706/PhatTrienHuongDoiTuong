namespace BaiTap1.Domain;

public enum LoanStatus { Borrowed, Returned }

public class Loan
{
    public Member Member { get; private set; }
    public Book Book { get; private set; }
    public DateTime DueDate { get; private set; }
    public LoanStatus Status { get; private set; } = LoanStatus.Borrowed;
    public int RenewCount { get; private set; }

    private Loan(Member member, Book book, DateTime today)
    {
        Member = member;
        Book = book;
        DueDate = today.AddDays(14);
    }

    public static Loan Create(Member member, Book book, DateTime today)
    {
        if (member == null) throw new ArgumentNullException(nameof(member));
        if (book == null) throw new ArgumentNullException(nameof(book));
        return new Loan(member, book, today);
    }

    public bool IsOverdue(DateTime today) =>
        Status == LoanStatus.Borrowed && today.Date > DueDate.Date;

    public void Renew(DateTime today, bool hasReservation)
    {
        if (IsOverdue(today))
            throw new InvalidOperationException("Đã quá hạn, không gia hạn được");
        if (RenewCount >= 1)
            throw new InvalidOperationException("Đã gia hạn rồi");
        if (hasReservation)
            throw new InvalidOperationException("Có người đặt trước, không gia hạn được");

        DueDate = DueDate.AddDays(7);
        RenewCount++;
    }

    public Fine? Return(DateTime today)
    {
        if (Status == LoanStatus.Returned)
            throw new InvalidOperationException("Sách đã được trả rồi");

        Status = LoanStatus.Returned;
        Book.GiveBack();

        int lateDays = (today.Date - DueDate.Date).Days;
        return lateDays > 0 ? Fine.Create(this, lateDays) : null;
    }
}