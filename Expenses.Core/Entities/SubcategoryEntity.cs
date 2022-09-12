namespace Expenses.Core.Entities
{
    public class SubcategoryEntity: CategoryEntity
    {
        public string CategoryName { get; set; } 

        public decimal Amount { get; set; }
    }
}