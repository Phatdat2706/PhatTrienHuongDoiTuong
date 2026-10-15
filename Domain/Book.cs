namespace BaiTap1.Domain;

public class Book
{
    public string Isbn { get; private set; }
    public string Title { get; private set; }
    public int TotalCopies { get; private set; }
    public int Available { get; private set; }

    private Book(string isbn, string title, int totalCopies)
    {
        Isbn = isbn;
        Title = title;
        TotalCopies = totalCopies;
        Available = totalCopies;
    }

    public static Book Create(string isbn, string title, int totalCopies)
    {
        if (string.IsNullOrWhiteSpace(isbn))
            throw new ArgumentException("ISBN trống");
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Tiêu đề trống");
        if (totalCopies <= 0)
            throw new ArgumentException("Số bản phải > 0");
        return new Book(isbn, title, totalCopies);
    }

    public void TakeOne()
    {
        if (Available <= 0)
            throw new InvalidOperationException("Sách đã hết bản");
        Available--;
    }

    public void GiveBack()
    {
        if (Available >= TotalCopies)
            throw new InvalidOperationException("Số bản trả vượt quá tổng số bản");
        Available++;
    }
}