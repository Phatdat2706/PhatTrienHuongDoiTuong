namespace BaiTap1.Domain;

public class Book
{
    public string Isbn { get; }
    public string Title { get; }
    public int Available { get; private set; }

    public Book(string isbn, string title, int totalCopies)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Tiêu đề trống");
        if (totalCopies <= 0)
            throw new ArgumentException("Số bản phải > 0");

        Isbn = isbn;
        Title = title;
        Available = totalCopies;
    }

    public void TakeOne()
    {
        if (Available <= 0)
            throw new InvalidOperationException("Sách đã hết bản");
        Available--;
    }

    public void GiveBack() => Available++;
}