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

        public async Task<EntryEntity> GetEntryAsync(string entryId)
        {
            BsonDocument bsonElements;
            PeriodEntity periodEntity;
            EntryEntity entity;

            bsonElements = BsonSerializer.Deserialize<BsonDocument>($"{{'ListEntries._id': ObjectId('{entryId}')}}");
            periodEntity = await _collection.Find(bsonElements).FirstOrDefaultAsync();
            if (periodEntity is null)
                entity = null;
            else
                entity = periodEntity.ListEntries.Where(x => x.Id == entryId).FirstOrDefault();

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

        public async Task UpdateAsync(PeriodEntity entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

        public bool Exists(string periodId)
        {
            long count;

            count = _collection.CountDocuments(x => x.Id == periodId);

            return count == 0 ? false : true;
        }

        public async Task<PeriodEntity> GetByEntryIdAsync(string entryId)
        {
            BsonDocument bsonElements;
            PeriodEntity periodEntity;

            bsonElements = BsonSerializer.Deserialize<BsonDocument>($"{{'ListEntries._id': ObjectId('{entryId}')}}");
            periodEntity = await _collection.Find(bsonElements).FirstOrDefaultAsync();
            periodEntity.ListEntries = periodEntity.ListEntries.Where(x=>x.IsActive == true).ToList();

            return periodEntity;
        }

        public async Task DeleteEntryAsync(string entryId)
        {
            BsonDocument bsonElements;
            PeriodEntity periodEntity;
            int index;

            bsonElements = BsonSerializer.Deserialize<BsonDocument>($"{{'ListEntries._id': ObjectId('{entryId}')}}");
            periodEntity = await _collection.Find(bsonElements).FirstOrDefaultAsync();
             index =  periodEntity.ListEntries.FindIndex(x=>x.Id == entryId);            
            periodEntity.ListEntries[index].IsActive = false;

            await UpdateAsync(periodEntity);
        }

        public async Task<string> GetByExpenseIdAsync(string expenseId)
        {
            BsonDocument bsonElements;
            PeriodEntity periodEntity;            

            bsonElements = BsonSerializer.Deserialize<BsonDocument>($"{{'ListExpenses._id': ObjectId('{expenseId}')}}");
            periodEntity = await _collection.Find(bsonElements).FirstOrDefaultAsync();

            return periodEntity.Id;
        }

        public async Task<PeriodEntity> GetPeriodByExpenseIdAsync(string expenseId)
        {
            BsonDocument bsonElements;
            PeriodEntity periodEntity;            

            bsonElements = BsonSerializer.Deserialize<BsonDocument>($"{{'ListExpenses._id': ObjectId('{expenseId}')}}");
            periodEntity = await _collection.Find(bsonElements).FirstOrDefaultAsync();

            return periodEntity;
        }
    }//end class
}