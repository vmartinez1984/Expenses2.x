namespace Expenses.Core.Entities
{
    public class  EntryEntity: BaseBEntity
    {        
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}