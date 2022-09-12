using AutoMapper;
using Expenses.Core.Dtos;
using Expenses.Core.Entities;
using Expenses.Core.Interfaces.BusinessLayer;
using Expenses.Core.Interfaces.Repository;

namespace Expenses.BusinessLayer
{
    public class CategoryBl : ICategoryBl
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public CategoryBl(
            IRepository repository,
            IMapper mapper
        )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<string> AddAsync(CategoryDtoIn item)
        {
            CategoryEntity entity;

            entity = _mapper.Map<CategoryEntity>(item);
            entity.Id = await _repository.Category.AddAsync(entity);

            return entity.Id;
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.Category.DeleteAsync(id);
        }

        public bool Exists(string name)
        {
            bool exists;

            exists = _repository.Category.Exists(name);

            return exists;
        }

        public async Task<CategoryDto> GetAsync(string id)
        {
            CategoryEntity entity;
            CategoryDto item;

            entity = await _repository.Category.GetAsync(id);
            item = _mapper.Map<CategoryDto>(entity);

            return item;
        }

        public async Task<List<CategoryDto>> GetAsync()
        {
            List<CategoryEntity> entities;
            List<CategoryDto> list;

            entities = await _repository.Category.GetAsync();
            list = _mapper.Map<List<CategoryDto>>(entities);

            return list;
        }

        public async Task UpdateAsync(string id, CategoryDtoIn item)
        {
            CategoryEntity entity;

            entity = await _repository.Category.GetAsync(id);
            entity.Name = item.Name;

            await _repository.Category.UpdateAsync(entity);
        }
    }
}