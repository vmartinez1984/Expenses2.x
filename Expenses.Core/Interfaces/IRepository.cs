using Expenses.Core.Entities;

namespace Expenses.Core.Interfaces.Repository
{
    public interface IRepository
    {
        public ICategoryRepository Category { get; }

        public ISubcategoryRepository Subcategory { get; }
        public IPeriodRepository Period { get; }        
    }

    public interface IPeriodRepository : IBaseRepository<PeriodEntity>
    {
        Task<ExpenseEntity> GetExpenseAsync(string id);
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