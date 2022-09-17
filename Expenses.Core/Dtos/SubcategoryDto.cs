using System.ComponentModel.DataAnnotations;
using Expenses.Core.Validators;

namespace Expenses.Core.Dtos
{
    public class SubcategoryDto : SubcategoryDtoIn
    {
        public string Id { get; set; }

        public DateTime DateRegistration { get; set; } = DateTime.Now;
    }

    public class SubcategoryDtoIn
    {
        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        [Required]
        [StringLength(25)]
        [CategoryExists]
        public string CategoryName { get; set; }

        [Required]
        [Range(1,5000)]
        public decimal Amount { get; set; }

        //public bool IsBudget { get; set; }
    }
}