using Expenses.Core.Interfaces.Repository;

namespace Expenses.Repository
{
    public class Repository : IRepository
    {
        public Repository(
            ICategoryRepository categoryRepository
            , ISubcategoryRepository subcategoryRepository            
            , IPeriodRepository periodRepository
        )
        {
            this.Category = categoryRepository;
            this.Subcategory = subcategoryRepository;            
            this.Period = periodRepository;
        }
        public ICategoryRepository Category { get; }

        public ISubcategoryRepository Subcategory { get; }

        public IPeriodRepository Period { get; }        
    }
}