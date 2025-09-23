namespace Domain.Entities
{
    public class Income
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string CompanyName { get; init; } = string.Empty;
        public decimal GrossSalary { get; set; }
        public int WorkingHoursPerMonth { get; init; }
        public int WorkingDaysPerMonth { get; init; }
        public decimal VAorVR { get; set; }
        public decimal PLR { get; set; }
        public DateOnly CreatedAt { get; init; }
        public DateOnly UpdateAt { get; set; }
        public decimal NetSalary => GrossSalary - INSSDiscount - IRDiscount;
        public decimal GSperHour => WorkingHoursPerMonth == 0 ? 0 : GrossSalary / WorkingHoursPerMonth;
        public decimal GSperDay => WorkingDaysPerMonth == 0 ? 0 : GrossSalary / WorkingDaysPerMonth;
        public decimal ThirteenthSalary => NetSalary;
        public decimal VAorVRDaily => WorkingDaysPerMonth == 0 ? 0 : VAorVR / WorkingDaysPerMonth;
        public decimal FGTSMonthly => GrossSalary * 0.08M;
        public decimal VAorVRAnnual => VAorVR * 12;
        public decimal INSSDiscount { get; private set; }
        public decimal IRDiscount { get; private set; }
        public decimal TotalIncomeAnnual => (NetSalary * 13) + PLR;

        public void UpdateINSSDiscount(decimal inss)
        {
            INSSDiscount = inss;
        }
        public void UpdateIRDiscount(decimal ir)
        {
            IRDiscount = ir;
        }
    }
}