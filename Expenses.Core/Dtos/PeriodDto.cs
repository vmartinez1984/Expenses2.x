namespace Expenses.Core.Dtos
{
    public class PeriodDto : PeriodDtoIn
    {
        public string Id { get; set; }

        public DateTime DateRegister { get; set; }
    }

    public class PeriodDtoIn
    {
        public string Name { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public decimal TotalExpenses { get; set; }

        public decimal TotalEntries { get; set; }

        public decimal Total { get { return TotalEntries - TotalExpenses; } }
    }
}