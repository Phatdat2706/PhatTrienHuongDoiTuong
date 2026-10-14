namespace BaiTap1.Domain;

public class Fine
{
    public const decimal RatePerDay = 2000m;

    public Loan Loan { get; }
    public decimal Amount { get; }

    public Fine(Loan loan, int lateDays)
    {
        Loan = loan;
        Amount = lateDays * RatePerDay;
    }
}