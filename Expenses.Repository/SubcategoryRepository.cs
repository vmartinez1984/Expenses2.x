using Expenses.Core.Entities;
using Expenses.Core.Interfaces.Repository;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Expenses.Repository
{
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly IMongoCollection<SubcategoryEntity> _collection;

        public SubcategoryRepository(IOptions<DbSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
           databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _collection = mongoDatabase.GetCollection<SubcategoryEntity>(
                databaseSettings.Value.SubcategoryCollection);
        }

        public async Task<string> AddAsync(SubcategoryEntity entity)
        {
            await _collection.InsertOneAsync(entity);

            return entity.Id;
        }

        public async Task DeleteAsync(string id)
        {
            SubcategoryEntity entity;

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

        public async Task<List<SubcategoryEntity>> GetAsync()
        {
            List<SubcategoryEntity> entities;

            entities = await _collection.Find(x => x.IsActive == true).ToListAsync();

            return entities;
        }

        public async Task<SubcategoryEntity> GetAsync(string id)
        {
            SubcategoryEntity entity;

            entity = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

            return entity;
        }

        public async Task<SubcategoryEntity> GetByNameAsync(string subcategoryName)
        {
            SubcategoryEntity entity;

            entity = await _collection.Find(x => x.Name == subcategoryName).FirstOrDefaultAsync();

            return entity;
        }

        public async Task UpdateAsync(SubcategoryEntity entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

    }//end class
}