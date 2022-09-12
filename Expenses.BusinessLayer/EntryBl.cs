using AutoMapper;
using Expenses.Core.Dtos;
using Expenses.Core.Entities;
using Expenses.Core.Interfaces.BusinessLayer;
using Expenses.Core.Interfaces.Repository;
using MongoDB.Bson;

namespace Expenses.BusinessLayer
{
    public class EntryBl : IEntryBl
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public EntryBl(
            IRepository repository,
            IMapper mapper
        )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<string> AddAsync(EntryDtoIn item)
        {
            EntryEntity entity;
            PeriodEntity periodEntity;

            entity = _mapper.Map<EntryEntity>(item);
            entity.Id = ObjectId.GenerateNewId().ToString();
            periodEntity = await _repository.Period.GetAsync(item.PeriodId);
            if (periodEntity.ListEntries is null)
                periodEntity.ListEntries = new List<EntryEntity>();
            periodEntity.ListEntries.Add(entity);
            periodEntity.TotalEntries = periodEntity.ListEntries.Sum(x => x.Amount);

            await _repository.Period.UpdateAsync(periodEntity);

            return entity.Id;
        }

        public async Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EntryDto>> GetAllAsync(string periodId)
        {
            List<EntryDto> list;
            PeriodEntity entity;

            entity = await _repository.Period.GetAsync(periodId);
            list = _mapper.Map<List<EntryDto>>(entity.ListEntries);

            return list;
        }

        public async Task<EntryDto> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EntryDto>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(string id, EntryDtoIn item)
        {
            throw new NotImplementedException();
        }
    }
}