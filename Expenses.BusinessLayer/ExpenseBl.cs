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

        public async Task<ExpenseDto> GetAsync(string expenseId)
        {
            ExpenseEntity entity;
            ExpenseDto item;

            entity = await _repository.Period.GetExpenseAsync(expenseId);
            item = _mapper.Map<ExpenseDto>(entity);
            item.PeriodId = await GetPeriodId(expenseId);

            return item;
        }

        private async Task<string> GetPeriodId(string expenseId)
        {
            string periodId;

            periodId = await _repository.Period.GetByExpenseIdAsync(expenseId);

            return periodId;
        }

        public async Task<List<ExpenseDto>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(string expenseId)
        {
            PeriodEntity periodEntity;
            int index;

            periodEntity = await _repository.Period.GetPeriodByExpenseIdAsync(expenseId);
            index = periodEntity.ListExpenses.FindIndex(x=>x.Id == expenseId);
            periodEntity.ListExpenses[index].IsActive = false;
           
            await _repository.Period.UpdateAsync(periodEntity);
        }

        public async Task<List<ExpenseDto>> GetAllAsync(string periodId)
        {
            List<ExpenseDto> list;
            PeriodEntity entity;

            entity = await _repository.Period.GetAsync(periodId);
            list = _mapper.Map<List<ExpenseDto>>(entity.ListExpenses);

            return list;
        }       

        public async Task UpdateAsync(string expenseId, ExpenseDtoIn item)
        {
            PeriodEntity periodEntity;
            int index;

            periodEntity = await _repository.Period.GetPeriodByExpenseIdAsync(expenseId);
            index = periodEntity.ListExpenses.FindIndex(x=>x.Id == expenseId);
            periodEntity.ListExpenses[index].Amount = item.Amount;
            periodEntity.ListExpenses[index].Name = item.Name;
            periodEntity.ListExpenses[index].SubcategoryName = item.SubcategoryName;
            periodEntity.ListExpenses[index].CategoryName = await GetCategoryNameAsync(item.SubcategoryName);

            await _repository.Period.UpdateAsync(periodEntity);
        }
    }
}