using Expenses.Core.Entities;
using Expenses.Core.Interfaces.Repository;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Expenses.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<CategoryEntity> _collection;

        public CategoryRepository(IOptions<DbSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
           databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _collection = mongoDatabase.GetCollection<CategoryEntity>(
                databaseSettings.Value.CategoryCollection);
        }

        public async Task<string> AddAsync(CategoryEntity entity)
        {
            await _collection.InsertOneAsync(entity);

            return entity.Id;
        }

        public async Task DeleteAsync(string id)
        {
            CategoryEntity entity;

            entity = await GetAsync(id);
            entity.IsActive = false;

            await UpdateAsync(entity);
        }

        public bool Exists(string name)
        {
            long count;

            count = _collection.CountDocuments(x => x.Name == name);

            return count == 0 ? false : true;
        }

        public async Task<List<CategoryEntity>> GetAsync()
        {
            List<CategoryEntity> entities;

            entities = await _collection.Find(x => x.IsActive == true).ToListAsync();

            return entities;
        }

        public async Task<CategoryEntity> GetAsync(string id)
        {
            CategoryEntity entity;

            entity = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

            return entity;
        }

        public async Task UpdateAsync(CategoryEntity entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

    }//end class
}