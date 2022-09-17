using System.ComponentModel.DataAnnotations;
using Expenses.Core.Validators;

namespace Expenses.Core.Dtos
{
    public class ExpenseDtoIn
    {
        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        [Required]
        [Range(1, 13000)]
        public decimal Amount { get; set; }

        [Required]
        [MinLength(24)]
        [MaxLength(24)]
        [PeriodIdExists]
        public string PeriodId { get; set; }

        [Required]
        [SubcategoryExist]
        public string SubcategoryName { get; set; }
    }

    public class ExpenseDto
    {
        public string Id { get; set; }
        
        public DateTime DateRegistration { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }
        
        public string SubcategoryName { get; set; }

        public string PeriodId { get; set; }
    }
}