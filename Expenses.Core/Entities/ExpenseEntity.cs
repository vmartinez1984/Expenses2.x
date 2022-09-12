namespace Expenses.Core.Entities
{
    public class ExpenseEntity: BaseBEntity
    {
        public string Name { get; set; }
        
        public string CategoryName { get; set; }

        public string SubcategoryName { get; set; }

        public decimal Amount { get; set; }
    }
}