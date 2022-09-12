namespace Expenses.Core.Entities
{
    public class PeriodEntity : BaseBEntity
    {
        public PeriodEntity()
        {
            this.ListExpenses = new List<ExpenseEntity>();
            this.ListEntries = new List<EntryEntity>();
        }
        public string Name { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public List<EntryEntity> ListEntries { get; set; }

        public List<ExpenseEntity> ListExpenses { get; set; }

        public decimal TotalExpenses
        { get; set; }

        public decimal TotalEntries
        { get; set; }

        public decimal Total { get; set; }
    }
}