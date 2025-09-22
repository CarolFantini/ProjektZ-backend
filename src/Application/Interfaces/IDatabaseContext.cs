namespace Application.Interfaces
{
    public interface IDatabaseContext
    {
        Task<List<T>> GetAllAsync<T>(string? collectionName = null) where T : class;
        Task AddAsync<T>(T entity, string? collectionName = null) where T : class;
        Task UpdateAsync<T>(T entity, string? collectionName = null) where T : class;
        Task DeleteAsync<T>(T entity, string? collectionName = null) where T : class;
    }
}
