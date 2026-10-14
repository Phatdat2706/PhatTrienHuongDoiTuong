namespace BaiTap1.Domain;

public enum ReservationStatus { Waiting }

public class Reservation
{
    public Member Member { get; }
    public Book Book { get; }
    public ReservationStatus Status { get; } = ReservationStatus.Waiting;

    public Reservation(Member member, Book book)
    {
        Member = member;
        Book = book;
    }
}