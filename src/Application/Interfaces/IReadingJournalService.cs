namespace Application.Interfaces
{
    public interface IReadingJournalService
    {
        Task<bool> SendEmail();
    }
}
