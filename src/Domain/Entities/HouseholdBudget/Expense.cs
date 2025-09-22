namespace Domain.Entities
{
    public class Expense
    {
        public string Name { get; init; } = string.Empty;
        public decimal Amount { get; init; }
        public bool IsFixed { get; init; }
        public bool IsEssential { get; init; }
        public bool isCreditCard { get; init; }
        public DateOnly? DueDay { get; init; }
        public DateTime? Renewal { get; init; }
        public DateTime CreatedAt { get; init; }
        public int? InstallmentCount { get; init; } // Total number of installments, if parcelled
        public bool IsInstallment { get; init; } // Indicates if this expense is parcelled
    }
}
