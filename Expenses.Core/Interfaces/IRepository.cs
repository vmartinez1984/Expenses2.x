using Expenses.Core.Entities;

namespace Expenses.Core.Interfaces.Repository
{
    public interface IRepository
    {
        public ICategoryRepository Category { get; }

        public ISubcategoryRepository Subcategory { get; }
        public IPeriodRepository Period { get; }
        public IEntryRepository Entry { get; }
    }

    public interface IEntryRepository
    {
        Task<EntryEntity> GetAsync(string id);
        Task UpdateAsync(EntryEntity entity);
        Task<string> AddAsync(string periodId, EntryEntity entity);
    }

    public interface IPeriodRepository : IBaseRepository<PeriodEntity>
    {
        Task<ExpenseEntity> GetExpenseAsync(string id);
        Task<EntryEntity> GetEntryAsync(string entryId);
        bool Exists(string periodId);
        Task<PeriodEntity> GetByEntryIdAsync(string entryId);
        Task DeleteEntryAsync(string entryId);
    }

    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetAsync(string id);
        Task<List<T>> GetAsync();
        Task<string> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string id);
    }

    public interface ICategoryRepository : IBaseRepository<CategoryEntity>
    {
        bool Exists(string name);
    }

    public interface ISubcategoryRepository : IBaseRepository<SubcategoryEntity>
    {
        bool Exists(string name);
        Task<SubcategoryEntity> GetByNameAsync(string subcategoryName);
    }
}