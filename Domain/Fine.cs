namespace BaiTap1.Domain;

public class Fine
{
    public const decimal RatePerDay = 2000m;

    public Loan Loan { get; private set; }
    public decimal Amount { get; private set; }

    private Fine(Loan loan, int lateDays)
    {
        Loan = loan;
        Amount = lateDays * RatePerDay;
    }

    public static Fine Create(Loan loan, int lateDays)
    {
        if (loan == null)
            throw new ArgumentNullException(nameof(loan));
        if (lateDays <= 0)
            throw new ArgumentException("Số ngày trễ phải > 0");
        return new Fine(loan, lateDays);
    }
}