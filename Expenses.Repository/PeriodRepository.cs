using Expenses.Core.Entities;
using Expenses.Core.Interfaces.Repository;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Expenses.Repository
{
    public class PeriodRepository : IPeriodRepository
    {
        private readonly IMongoCollection<PeriodEntity> _collection;

        public PeriodRepository(IOptions<DbSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
           databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _collection = mongoDatabase.GetCollection<PeriodEntity>(
                databaseSettings.Value.PeriodCollection);
        }

        public async Task<string> AddAsync(PeriodEntity entity)
        {
            await _collection.InsertOneAsync(entity);

            return entity.Id;
        }

        public async Task DeleteAsync(string id)
        {
            PeriodEntity entity;

            entity = await GetAsync(id);
            entity.IsActive = false;

            await UpdateAsync(entity);
        }

        public async Task<List<PeriodEntity>> GetAsync()
        {
            List<PeriodEntity> entities;

            entities = await _collection.Find(x => x.IsActive == true).ToListAsync();

            return entities;
        }

        public async Task<PeriodEntity> GetAsync(string id)
        {
            PeriodEntity entity;

            entity = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            entity.ListEntries = entity.ListEntries.Where(x => x.IsActive == true).ToList();
            entity.ListExpenses = entity.ListExpenses.Where(x => x.IsActive == true).ToList();

            return entity;
        }

        public async Task<ExpenseEntity> GetExpenseAsync(string id)
        {
            //{"ListEntries._id": ObjectId('631a74a1a11bb6bc7e73b691')}
            BsonDocument bsonElements;
            PeriodEntity periodEntity;
            ExpenseEntity expenseEntity;

            bsonElements = BsonSerializer.Deserialize<BsonDocument>($"{{'ListExpenses._id': ObjectId('{id}')}}");
            periodEntity = await _collection.Find(bsonElements).FirstOrDefaultAsync();
            if (periodEntity is null)
                expenseEntity = null;
            else
                expenseEntity = periodEntity.ListExpenses.Where(x => x.Id == id).FirstOrDefault();

            return expenseEntity;
        }

        // public async Task<ExpenseEntity> GetExpenseAsync(string expenseId)
        // {
        //     ExpenseEntity expenseEntity;

        //     expenseEntity = await _collection.Find().FirstOrDefault();
        // }

        public async Task UpdateAsync(PeriodEntity entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

    }//end class
}