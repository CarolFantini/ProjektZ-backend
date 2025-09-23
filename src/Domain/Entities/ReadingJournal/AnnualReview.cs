namespace Domain.Entities
{
    public class AnnualReview
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public List<Book>? BooksReads { get; set; }
        public decimal TotalAmountSpentOnBooks { get; set; }
        public int ReadingGoal { get; set; }
        public int TotalPagesRead
        {
            get
            {
                return BooksReads?.Sum(b => b.Pages) ?? 0;
            }
        }
    }
}
