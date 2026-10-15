namespace BaiTap1.Domain;

public enum ReservationStatus { Waiting }

public class Reservation
{
    public Member Member { get; private set; }
    public Book Book { get; private set; }
    public ReservationStatus Status { get; private set; } = ReservationStatus.Waiting;

    private Reservation(Member member, Book book)
    {
        Member = member;
        Book = book;
    }

    public static Reservation Create(Member member, Book book)
    {
        if (member == null) throw new ArgumentNullException(nameof(member));
        if (book == null) throw new ArgumentNullException(nameof(book));
        return new Reservation(member, book);
    }
}