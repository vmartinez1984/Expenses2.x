using Expenses.Core.Dtos;

namespace Expenses.Core.Interfaces.BusinessLayer
{
    public interface IUnitOfWork
    {
        public IPeriodBl Period { get; }

        public ICategoryBl Category { get; }

        public ISubcategoryBl Subcategory { get; }

        public IEntryBl Entry { get; }
        
        public IExpenseBl Expense { get; }
    }

    public interface IExpenseBl : IBaseABl<ExpenseDtoIn, ExpenseDto>
    {
        Task<List<ExpenseDto>> GetAllAsync(string periodId);
    }

    public interface IEntryBl : IBaseABl<EntryDtoIn, EntryDto>
    {
        Task<List<EntryDto>> GetAllAsync(string periodId);
    }

    /// <summary>
    /// Donde T es la entrada y U la salida
    /// </summary>
    /// <typeparam name="T">Entrada</typeparam>
    /// <typeparam name="U">Salida</typeparam>
    public interface IBaseABl<T, U> where T : class
    {
        Task<string> AddAsync(T item);
        Task DeleteAsync(string id);
        Task<U> GetAsync(string id);
        Task UpdateAsync(string id, T item);
    }

    /// <summary>
    /// Donde T es la entrada y U la salida
    /// </summary>
    /// <typeparam name="T">Entrada</typeparam>
    /// <typeparam name="U">Salida</typeparam>
    public interface IBaseBl<T, U> where T : class
    {
        Task<string> AddAsync(T item);
        Task DeleteAsync(string id);
        Task<U> GetAsync(string id);
        Task<List<U>> GetAsync();
        Task UpdateAsync(string id, T item);
    }

    public interface IPeriodBl
    {
        /// <summary>
        /// Retunr list of Periods
        /// </summary>
        /// <returns></returns>        
        Task<List<PeriodDto>> GetAsync();
        Task<PeriodDto> GetAsync(string id);
        Task<string> AddAsync(PeriodDtoIn period);
        Task UpdateAsync(string id, PeriodDto period);
    }

    public interface ICategoryBl : IBaseBl<CategoryDtoIn, CategoryDto>
    {
        bool Exists(string name);
    }

    public interface ISubcategoryBl : IBaseBl<SubcategoryDtoIn, SubcategoryDto>
    {
        bool Exists(string name);
        Task<List<SubcategoryDto>> GetByCategoryIdAsync(string id);        
    }
}