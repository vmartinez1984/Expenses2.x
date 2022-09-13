using AutoMapper;
using Expenses.Core.Dtos;
using Expenses.Core.Entities;
using Expenses.Core.Interfaces.BusinessLayer;
using Expenses.Core.Interfaces.Repository;

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

            entity = _mapper.Map<EntryEntity>(item);
            entity.Id = await _repository.Entry.AddAsync(item.PeriodId, entity);

            return entity.Id;
        }

        public async Task DeleteAsync(string entryId)
        {
            await _repository.Period.DeleteEntryAsync(entryId);
        }

        public async Task<List<EntryDto>> GetAllAsync(string periodId)
        {
            List<EntryDto> list;
            PeriodEntity entity;

            entity = await _repository.Period.GetAsync(periodId);
            list = _mapper.Map<List<EntryDto>>(entity.ListEntries);

            return list;
        }

        public async Task<EntryDto> GetAsync(string entryId)
        {
            EntryEntity entity;
            EntryDto item;
            PeriodEntity periodEntity;

            periodEntity = await _repository.Period.GetByEntryIdAsync(entryId);
            entity = periodEntity.ListEntries.Where(x => x.Id == entryId).FirstOrDefault();
            item = _mapper.Map<EntryDto>(entity);
            if (entity is not null)
                item.PeriodId = periodEntity.Id;

            return item;
        }

        public async Task UpdateAsync(string id, EntryDtoIn item)
        {
            EntryEntity entity;

            entity = await _repository.Entry.GetAsync(id);
            entity.Amount = item.Amount;
            entity.Name = item.Name;

            await _repository.Entry.UpdateAsync(entity);
        }
    }
}