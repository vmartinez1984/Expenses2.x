using AutoMapper;
using Expenses.Core.Dtos;
using Expenses.Core.Entities;
using Expenses.Core.Interfaces.BusinessLayer;
using Expenses.Core.Interfaces.Repository;
using MongoDB.Bson;

namespace Expenses.BusinessLayer
{
    public class ExpenseBl : IExpenseBl
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public ExpenseBl(
            IRepository repository,
            IMapper mapper
        )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<string> AddAsync(ExpenseDtoIn item)
        {
            ExpenseEntity entity;
            PeriodEntity periodEntity;

            entity = _mapper.Map<ExpenseEntity>(item);
            entity.Id = ObjectId.GenerateNewId().ToString();
            entity.CategoryName = await GetCategoryNameAsync(entity.SubcategoryName);
            periodEntity = await _repository.Period.GetAsync(item.PeriodId);
            if (periodEntity.ListExpenses is null)
                periodEntity.ListExpenses = new List<ExpenseEntity>();
            periodEntity.ListExpenses.Add(entity);
            periodEntity.TotalEntries = periodEntity.ListExpenses.Sum(x => x.Amount);

            await _repository.Period.UpdateAsync(periodEntity);

            return entity.Id;
        }

        private async Task<string> GetCategoryNameAsync(string subcategoryName)
        {
            SubcategoryEntity entity;

            entity = await _repository.Subcategory.GetByNameAsync(subcategoryName);

            return entity.CategoryName;
        }

        public async Task<ExpenseDto> GetAsync(string id)
        {
            ExpenseEntity entity;
            ExpenseDto item;

            entity = await _repository.Period.GetExpenseAsync(id);
            item = _mapper.Map<ExpenseDto>(entity);

            return item;
        }

        public async Task<List<ExpenseDto>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ExpenseDto>> GetAllAsync(string periodId)
        {
            List<ExpenseDto> list;
            PeriodEntity entity;

            entity = await _repository.Period.GetAsync(periodId);
            list = _mapper.Map<List<ExpenseDto>>(entity.ListExpenses);

            return list;
        }       

        public async Task UpdateAsync(string id, ExpenseDtoIn item)
        {
            throw new NotImplementedException();
        }
    }
}