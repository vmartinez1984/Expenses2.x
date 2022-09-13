using Expenses.Core.Entities;
using Expenses.Core.Interfaces.Repository;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Expenses.Repository
{
    public class EntryRepository : IEntryRepository
    {
        private readonly IMongoCollection<PeriodEntity> _collection;

        public EntryRepository(IOptions<DbSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
           databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _collection = mongoDatabase.GetCollection<PeriodEntity>(
                databaseSettings.Value.PeriodCollection);
        }

        public async Task<EntryEntity> GetAsync(string id)
        {
            //{"ListEntries._id": ObjectId('631a74a1a11bb6bc7e73b691')}
            BsonDocument bsonElements;
            PeriodEntity periodEntity;
            EntryEntity expenseEntity;

            bsonElements = BsonSerializer.Deserialize<BsonDocument>($"{{'ListEntries._id': ObjectId('{id}')}}");
            periodEntity = await _collection.Find(bsonElements).FirstOrDefaultAsync();
            if (periodEntity is null)
                expenseEntity = null;
            else
                expenseEntity = periodEntity.ListEntries.Where(x => x.Id == id).FirstOrDefault();

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


        public async Task DeleteAsync(string entryId)
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

        public Task UpdateAsync(EntryEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<string> AddAsync(string periodId, EntryEntity entity)
        {            
            PeriodEntity periodEntity;
          
            entity.Id = ObjectId.GenerateNewId().ToString();
            periodEntity = await _collection.Find(x=> x.Id == periodId).FirstOrDefaultAsync();
            if (periodEntity.ListEntries is null)
                periodEntity.ListEntries = new List<EntryEntity>();
            periodEntity.ListEntries.Add(entity);
            periodEntity.TotalEntries = periodEntity.ListEntries.Sum(x => x.Amount);

            await _collection.ReplaceOneAsync(x => x.Id == periodId, periodEntity);

            return entity.Id;
        }
    }//end class
}