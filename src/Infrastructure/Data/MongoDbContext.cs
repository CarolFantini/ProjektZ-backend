using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class MongoDbContext : IDatabaseContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IMongoDatabase database)
        {
            _database = database;
        }

        private IMongoCollection<T> GetCollection<T>(string name) =>
            _database.GetCollection<T>(name ?? typeof(T).Name);

        public async Task<List<T>> GetAllAsync<T>(string? collectionName = null) where T : class
        {
            return await GetCollection<T>(collectionName!).Find(_ => true).ToListAsync();
        }

        public async Task AddAsync<T>(T entity, string? collectionName = null) where T : class
        {
            await GetCollection<T>(collectionName!).InsertOneAsync(entity);
        }

        public async Task UpdateAsync<T>(T entity, string? collectionName = null) where T : class
        {
            var collection = GetCollection<T>(collectionName!);
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty == null) throw new Exception("MongoDB entity must have Id property");
            var id = idProperty.GetValue(entity);
            var filter = Builders<T>.Filter.Eq("Id", id);
            await collection.ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteAsync<T>(T entity, string? collectionName = null) where T : class
        {
            var collection = GetCollection<T>(collectionName!);
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty == null) throw new Exception("MongoDB entity must have Id property");
            var id = idProperty.GetValue(entity);
            var filter = Builders<T>.Filter.Eq("Id", id);
            await collection.DeleteOneAsync(filter);
        }
    }
}
