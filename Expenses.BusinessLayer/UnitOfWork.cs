using Expenses.Core.Interfaces.BusinessLayer;

namespace Expenses.BusinessLayer;
public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(
        ICategoryBl categoryBl
        , ISubcategoryBl subcategoyBl
        , IPeriodBl periodBl
        , IEntryBl entryBl
        , IExpenseBl expenseBl
    )
    {
        this.Category = categoryBl;
        this.Subcategory = subcategoyBl;
        this.Period = periodBl;
        this.Entry = entryBl;
        this.Expense = expenseBl;
    }
    public IPeriodBl Period { get; }

    public ICategoryBl Category { get; }

    public ISubcategoryBl Subcategory { get; }

    public IEntryBl Entry { get; }

    public IExpenseBl Expense { get; }
}