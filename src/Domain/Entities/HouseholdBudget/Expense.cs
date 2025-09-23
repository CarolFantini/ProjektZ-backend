namespace Domain.Entities
{
    public class Expense
    {
        public int Id { get; set; }
        public string Name { get; init; } = string.Empty;
        public decimal Amount { get; set; }
        public bool IsFixed { get; init; }
        public bool IsEssential { get; init; }
        public bool isCreditCard { get; set; }
        public bool IsActive { get; set; }
        public DateOnly? DueDay { get; set; }
        public DateOnly? Renewal { get; set; }
        public DateOnly CreatedAt { get; init; }
        public int? InstallmentCount { get; init; } // Total number of installments, if parcelled
        public bool IsInstallment { get; init; } // Indicates if this expense is parcelled
    }
}
