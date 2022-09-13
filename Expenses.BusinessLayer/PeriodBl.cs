using AutoMapper;
using Expenses.Core.Dtos;
using Expenses.Core.Entities;
using Expenses.Core.Interfaces.BusinessLayer;
using Expenses.Core.Interfaces.Repository;

namespace Expenses.BusinessLayer
{
    public class PeriodBl : IPeriodBl
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public PeriodBl(
            IRepository repository,
            IMapper mapper
        )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<string> AddAsync(PeriodDtoIn item)
        {
            PeriodEntity entity;

            entity = _mapper.Map<PeriodEntity>(item);
            entity.Id = await _repository.Period.AddAsync(entity);

            return entity.Id;
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.Category.DeleteAsync(id);
        }

        public bool Exists(string periodId)
        {
            bool exists;

            exists =  _repository.Period.Exists(periodId);

            return exists;
        }

        public async Task<PeriodDto> GetAsync(string id)
        {
            PeriodEntity entity;
            PeriodDto item;

            entity = await _repository.Period.GetAsync(id);
            item = _mapper.Map<PeriodDto>(entity);

            return item;
        }

        public async Task<List<PeriodDto>> GetAsync()
        {
            List<PeriodEntity> entities;
            List<PeriodDto> list;

            entities = await _repository.Period.GetAsync();
            list = _mapper.Map<List<PeriodDto>>(entities);

            return list;
        }

        public async Task UpdateAsync(string id, PeriodDto item)
        {
            PeriodEntity entity;

            entity = await _repository.Period.GetAsync(id);
            entity.Name = item.Name;

            await _repository.Period.UpdateAsync(entity);
        }
    }
}