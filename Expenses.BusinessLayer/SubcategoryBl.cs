using AutoMapper;
using Expenses.Core.Dtos;
using Expenses.Core.Entities;
using Expenses.Core.Interfaces.BusinessLayer;
using Expenses.Core.Interfaces.Repository;

namespace Expenses.BusinessLayer
{
    public class SubcategoryBl : ISubcategoryBl
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public SubcategoryBl(
            IRepository repository,
            IMapper mapper
        )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<string> AddAsync(SubcategoryDtoIn item)
        {
            SubcategoryEntity entity;

            entity = _mapper.Map<SubcategoryEntity>(item);
            entity.Id = await _repository.Subcategory.AddAsync(entity);

            return entity.Id;
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.Subcategory.DeleteAsync(id);
        }

        public bool Exists(string name)
        {
            bool exists;

            exists =  _repository.Subcategory.Exists(name);

            return exists;
        }

        public async Task<SubcategoryDto> GetAsync(string id)
        {
            SubcategoryEntity entity;
            SubcategoryDto item;

            entity = await _repository.Subcategory.GetAsync(id);
            item = _mapper.Map<SubcategoryDto>(entity);

            return item;
        }

        public async Task<List<SubcategoryDto>> GetAsync()
        {
            List<SubcategoryEntity> entities;
            List<SubcategoryDto> list;

            entities = await _repository.Subcategory.GetAsync();
            list = _mapper.Map<List<SubcategoryDto>>(entities);

            return list;
        }

        public async Task<List<SubcategoryDto>> GetByCategoryIdAsync(string categoryId)
        {
            List<SubcategoryEntity> entities;
            List<SubcategoryDto> list;
            string categoryName;

            entities = await _repository.Subcategory.GetAsync();
            list = _mapper.Map<List<SubcategoryDto>>(entities);
            categoryName = (await _repository.Category.GetAsync(categoryId)).Name;
            list = list.Where(x=> x.CategoryName == categoryName).ToList();

            return list;
        }

        public async Task UpdateAsync(string id, SubcategoryDtoIn item)
        {
            SubcategoryEntity entity;

            entity = await _repository.Subcategory.GetAsync(id);
            entity.Name = item.Name;
            entity.Amount = item.Amount;
            entity.CategoryName = item.CategoryName;

            await _repository.Subcategory.UpdateAsync(entity);
        }

    }//end class
}